using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    // 移動関連
    public float moveSpeed;
    public DialogueManager manager;
    float speedX, speedY;

    // 体力関連
    public int health;
    public int num0fHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    // 無敵システム関連
    public float invincibleTime = 2.0f;
    private bool invincible = false;
    private float invincibleTimer = 2.0f;

    // ノックバックシステム関連
    public float knockbackForce = 10f;
    private Vector2 knockbackDirection;
    private bool isKnockback = false;

    // アニメーション関連
    bool isstart;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Rigidbody2D rb;
    private Animator anim;

    private bool isInputEnabled = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (!isInputEnabled)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        anim.SetFloat("Speed", rb.velocity.magnitude);
        isstart = anim.GetBool("IsStart?");

        // 現在の体力が最大体力を超えないように制限
        if (health > num0fHearts)
        {
            health = num0fHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i < health + 0.5f)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // ハートが最大体力を超えないように有効/無効設定
            hearts[i].enabled = i < num0fHearts;
        }

        if (!isKnockback) // ノックバック中でないときのみ移動
        {
            speedX = Input.GetAxisRaw("Horizontal");
            speedY = Input.GetAxisRaw("Vertical");
            Vector2 moveDirection = new Vector2(speedX, speedY).normalized; // 正規化
            rb.velocity = moveDirection * moveSpeed;
        }

        if (invincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                invincible = false;
                spriteRenderer.color = originalColor;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            knockbackDirection = (transform.position - collision.transform.position).normalized;
            TakeDamage(1);
            Debug.Log("Player hit by enemy bullet");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            knockbackDirection = (transform.position - collision.transform.position).normalized;
            TakeDamage(1);
            Debug.Log("Player getting 10");
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            health -= damage;
            invincible = true;
            invincibleTimer = invincibleTime;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

            StartCoroutine(Knockback(0.2f, knockbackForce));

            if (health <= 0)
            {
                Debug.Log("Player Died");
                Die();
            }
        }
    }

    public IEnumerator Knockback(float dur, float power)
    {
        float timer = 0f;
        isKnockback = true;

        while (timer <= dur)
        {
            timer += Time.deltaTime;
            rb.AddForce(knockbackDirection * power, ForceMode2D.Impulse);
            yield return null;
        }

        isKnockback = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void EnableInput()
    {
        isInputEnabled = true;
    }

    public void DisableInput()
    {
        isInputEnabled = false;
    }
}
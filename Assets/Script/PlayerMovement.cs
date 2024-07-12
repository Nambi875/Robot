using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    //이동 관련
    public float moveSpeed;
    public DialogueManager manager;
    float speedX, speedY;

    //체력 관련
    public int health;
    public int num0fHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    //무적시스템관련
    public float invincibleTime = 2.0f;
    private bool invincible = false;
    private float invincibleTimer = 2.0f;

    //넉백시스템관련
    public float knockbackForce = 10f;
    private Vector2 knockbackDirection;
    private bool isKnockback = false;

    //애니메이션 관련
    bool isstart;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        anim.SetFloat("Speed", rb.velocity.magnitude);
        isstart = anim.GetBool("IsStart?");

        // 현재 체력이 최대 체력을 넘지 않도록 제한
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

            // 하트가 최대 체력을 넘지 않도록 활성화/비활성화 설정
            hearts[i].enabled = i < num0fHearts;
        }

        if (!isKnockback) // ｳﾋｹ・ﾁﾟﾀﾌ ｾﾆｴﾒ ｶｧｸｸ ﾀﾌｵｿ ｰ｡ｴﾉ
        {
            speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
            rb.velocity = new Vector2(speedX, speedY);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Start"))
        {
            moveSpeed = 0.0f;
        }
        else
        {
            moveSpeed = 3.0f;
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            knockbackDirection = (transform.position - collision.transform.position).normalized;
            TakeDamage(1);
            Debug.Log("Player getting 10");
        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            knockbackDirection = (transform.position - collision.transform.position).normalized;
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
}
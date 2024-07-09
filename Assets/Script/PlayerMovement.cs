using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float moveSpeed;
    public DialogueManager manager;
    public float health = 6.0f;
    public float invincibleTime = 2.0f;
    public float knockbackForce = 10f;
    private bool invincible = false;
    private float invincibleTimer = 2.0f;
    float speedX, speedY;
    bool isstart;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 knockbackDirection;
    private bool isKnockback = false;

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

        if (!isKnockback) // 넉백 중이 아닐 때만 이동 가능
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
    }

    void TakeDamage(int damage)
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
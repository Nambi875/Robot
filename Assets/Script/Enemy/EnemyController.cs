using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health ;
    public float detectionRadius ;
    public float obstacleAvoidanceRadius;
    public float speed;
    public float knockbackForce;
    public Transform player;
    public GameObject hangun;

    private bool isChasing;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private Vector2 knockbackDirection;
    private bool isKnockback;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isKnockback) return;

        float distanceToPlayerSqr = (player.position - transform.position).sqrMagnitude;
        bool playerInRange = distanceToPlayerSqr < detectionRadius * detectionRadius;

        if (playerInRange != isChasing)
        {
            isChasing = playerInRange;
            anim.SetBool("IsChasing", isChasing);
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, obstacleAvoidanceRadius, direction, detectionRadius);


        Debug.DrawLine(transform.position, player.position, Color.green);
        Debug.DrawRay(transform.position, direction * detectionRadius, Color.red);
        Debug.DrawRay(transform.position, direction * obstacleAvoidanceRadius, Color.blue);

        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            direction += hit.normal * obstacleAvoidanceRadius;
            direction.Normalize();
        }

        rb.velocity = direction * speed;
    }

    private void LateUpdate()
    {
        if (isChasing)
        {
            spriteRenderer.flipX = player.position.x < rb.position.x;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, obstacleAvoidanceRadius);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;  // ﾃｼｷﾂ ｰｨｼﾒ
        knockbackDirection = (transform.position - player.position).normalized;

        if (health <= 0)
        {
            knockbackForce = 2;
            StartCoroutine(Knockback(0.2f, knockbackForce));
            Die();
        }
        else
        {
            StartCoroutine(Knockback(0.2f, knockbackForce));
        }
    }

    void Die()
    {
        anim.SetBool("IsDead", true);
        hangun.gameObject.SetActive(false);
        Destroy(gameObject, 2);
    }

    private IEnumerator Knockback(float duration, float power)
    {
        isKnockback = true;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            rb.velocity = knockbackDirection * power;
            yield return null;
        }

        isKnockback = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAction playerAction = collision.gameObject.GetComponent<PlayerAction>();
            if (playerAction != null && !playerAction.isInvincible)
            {
                playerAction.TakeDamage(1, transform.position);
            }
        }
    }
}
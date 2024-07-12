using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f;  // ﾃﾖｴ・ﾃｼｷﾂ
    public float detectionRadius = 5.0f; // ﾇﾃｷｹﾀﾌｾ・ｰｨﾁ・ｹ・ｧ
    public float obstacleAvoidanceRadius = 1.0f; // ﾀ蠕ﾖｹｰ ﾈｸﾇﾇ ｹﾝｰ・
    public float speed; // ﾀ釥ﾇ ﾀﾌｵｿ ｼﾓｵｵ
    public float knockbackForce = 10f; // ｳﾋｹ・ﾈ・
    public Transform player; // 플레이어의 Transform을 참조

    private bool isChasing = false; // ﾇﾃｷｹﾀﾌｾ鋕ｦ ﾂﾑｴﾂ ｻﾂ
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private Vector2 knockbackDirection;
    private bool isKnockback = false;

    void Awake() // ﾃﾊｱ篳ｭ
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing && !isKnockback)
        {
            ChasePlayer();
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("IsChasing", false);
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, obstacleAvoidanceRadius, direction, detectionRadius);

        // ｵﾗｿ・ｷﾎｱﾗ ﾃﾟｰ｡
        Debug.DrawLine(transform.position, player.position, Color.green); // ﾇﾃｷｹﾀﾌｾ・ｹ貮・
        Debug.DrawRay(transform.position, direction * detectionRadius, Color.red); // ﾅｽﾁ・ｹ貮・
        Debug.DrawRay(transform.position, direction * obstacleAvoidanceRadius, Color.blue); // ﾈｸﾇﾇ ｹﾝｰ・

        anim.SetBool("IsChasing", true);

        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            Vector2 hitNormal = hit.normal;
            direction += hitNormal * obstacleAvoidanceRadius;
            direction.Normalize();
            Debug.Log("Obstacle detected! Adjusting direction.");
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
        speed = 0;
        anim.SetBool("IsDead", true);
        Destroy(gameObject,2);  // ﾁﾗｾ惕ｻ ｶｧ
    }

    public IEnumerator Knockback(float duration, float power)
    {
        float timer = 0f;
        isKnockback = true;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            rb.AddForce(knockbackDirection * power, ForceMode2D.Impulse);
            yield return null;
        }

        isKnockback = false;
    }
}



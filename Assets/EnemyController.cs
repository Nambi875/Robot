using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f;  // 최대 체력
    public float detectionRadius = 5.0f; // 플레이어 감지 범위
    public float obstacleAvoidanceRadius = 1.0f; // 장애물 회피 반경
    public float speed; // 적의 이동 속도
    public float knockbackForce = 10f; // 넉백 힘

    private bool isChasing = false; // 플레이어를 쫓는 상태
    private Transform player; // 플레이어의 Transform
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private Vector2 knockbackDirection;
    private bool isKnockback = false;

    void Awake() // 초기화
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

        // 디버그용 로그 추가
        Debug.DrawLine(transform.position, player.position, Color.green); // 플레이어 방향
        Debug.DrawRay(transform.position, direction * detectionRadius, Color.red); // 탐지 방향
        Debug.DrawRay(transform.position, direction * obstacleAvoidanceRadius, Color.blue); // 회피 반경

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
        health -= damage;  // 체력 감소
        knockbackDirection = (transform.position - player.position).normalized;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Knockback(0.2f, knockbackForce));
        }
    }

    void Die()
    {
        Destroy(gameObject);  // 죽었을 때
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



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float followRange = 10f;
    public float wanderRadius = 5f;
    public float wanderTimer = 5f;

    private Rigidbody2D rb;
    private float timer;
    private Vector2 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Vector2.Distance(transform.position, player.position) <= followRange)
        {
            targetPosition = player.position;
        }
        else if (timer >= wanderTimer)
        {
            targetPosition = (Vector2)transform.position + Random.insideUnitCircle * wanderRadius;
            timer = 0;
        }

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * 2f;  // 이동 속도 설정
    }

    void FixedUpdate()
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);

        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            // 장애물을 피하기 위해 새로운 방향을 설정
            direction += new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        rb.velocity = direction * 2f;  // 이동 속도 설정
    }
}
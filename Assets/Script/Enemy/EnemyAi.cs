using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("-----Enemy Detected------")]
    public Transform player;
    public float followRange = 10f;
    public float wanderRadius = 5f;
    public float wanderTimer = 5f;
    private float timer;

    [Header("-----Enemy Bullet------")]
    public GameObject bulletPrefab;  // 총알 프리팹
    public Transform firePoint;      // 총알 발사 위치
    public float fireRate = 1f;      // 총알 발사 간격 (초)
    public float fireRange = 10f;    // 사격 범위
    public float bulletspeed;
    private float nextFireTime = 0f;  

    private Rigidbody2D rb;
    private Vector2 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

        if (Vector2.Distance(transform.position, player.position) <= fireRange)
        {
            if (Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Fire();
            }
        }

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * 2f;  // 이동 속도 설정
    }

    void Fire()
    {
        Vector2 direction = (player.position - firePoint.position).normalized; // 올바른 방향 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletspeed;  // 총알 속도 조정
        }
    }
}
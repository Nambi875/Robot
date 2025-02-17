using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour
{
    public GameObject bulletPrefab;  // 총알 프리팹
    public Transform firePoint;      // 총알 발사 위치
    public float fireRate = 1f;      // 총알 발사 간격 (초)
    public float fireRange = 10f;    // 사격 범위
    private float nextFireTime = 0f;
    private Transform player;
    public float speed;
    AllAudio allAudio;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        allAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AllAudio>();
    }

    void Update()
    {
        nextFireTime += Time.deltaTime;
        if (Vector2.Distance(transform.position, player.position) <= fireRange)
        {
            if (fireRate <= nextFireTime)
            {
                nextFireTime = 0;
                Debug.Log("Shoot!");
                Fire();
            }
        }
    }

    void Fire()
    {
        allAudio.PlaySFX(allAudio.enemyfiresound);
        Vector2 direction = (player.position - firePoint.position).normalized; // 올바른 방향 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        EnemyBulletController ebc = bullet.GetComponent<EnemyBulletController>();

        ebc.Initialize(player,speed);

    }
}

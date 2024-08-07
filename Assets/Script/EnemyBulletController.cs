using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;
    public Animator anim;
    private int damage = 1;

    void Start()
    {
        // 일정 시간이 지나면 총알을 파괴
        Destroy(gameObject, lifetime);

        // 적과의 충돌 무시
        Collider2D bulletCollider = GetComponent<Collider2D>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
            if (enemyCollider != null)
            {
                Physics2D.IgnoreCollision(bulletCollider, enemyCollider);
            }
        }
    }

    void Update()
    {
        // 총알을 위로 이동
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void Awake()
    {
        // 애니메이터 초기화
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // 충돌한 오브젝트가 적인 경우 충돌 무시
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        // 플레이어와 충돌한 경우
        PlayerAction player = hitInfo.GetComponent<PlayerAction>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject, 0.05f);
            return;
        }

        // 벽과 충돌한 경우
        if (hitInfo.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
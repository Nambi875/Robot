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
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // 충돌한 오브젝트가 플레이어인지 확인
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            // 플레이어와 충돌 시 아무 동작도 하지 않음
            return;
        }
        PlayerAction player = hitInfo.GetComponent<PlayerAction>();

        if (hitInfo.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject,0.05f);
        }
    }
}
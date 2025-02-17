using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float speed;
    public float lifetime;
    Transform target;
    public Animator anim;

    public void Initialize(Transform player,float speeds)
    {
        Destroy(gameObject, lifetime);
        speed  = speeds;
        
    }
   private void Awake()
    {
        // 애니메이터 초기화
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
   
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌한 경우
            PlayerAction player = hitInfo.GetComponent<PlayerAction>();
            if (player != null)
            {
                player.TakeDamage(1, transform.position);
                Destroy(gameObject, 0.05f);
                return;
            }
        }

        // 벽과 충돌한 경우
        if (hitInfo.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
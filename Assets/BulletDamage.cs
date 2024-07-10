using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damage = 10f;  // 총알의 데미지

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // 충돌한 오브젝트가 플레이어인지 확인
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌 시 아무 동작도 하지 않음
            return;
        }

        if (hitInfo.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        // 충돌한 오브젝트가 적인지 확인
        EnemyController enemy = hitInfo.GetComponent<EnemyController>();
        TB01_Boss_Script TB01 = hitInfo.GetComponent<TB01_Boss_Script>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (TB01 != null)
        {
            TB01.TakeDamage(damage);
            Destroy(gameObject);
        }


        // 충돌 시 총알 제거

    }
}
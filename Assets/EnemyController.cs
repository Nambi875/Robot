using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f;  // ?? ??

    public void TakeDamage(float damage)
    {
        health -= damage;  // ??? ??
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);  // ??? 0 ??? ? ? ??
    }
}

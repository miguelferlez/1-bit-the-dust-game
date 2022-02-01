using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (collision.gameObject.layer == 8)
        {
            playerHealth.TakeDamage(2);
        }
        if (collision.gameObject.layer == 9)
        {
            enemyHealth.Die();
        }
    }
}

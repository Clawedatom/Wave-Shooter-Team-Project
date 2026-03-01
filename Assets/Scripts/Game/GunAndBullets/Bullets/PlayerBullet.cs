using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BaseBullet
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.gameObject.GetComponent<EnemyManager>() != null)
        {
            EnemyManager enemyManager = other.gameObject.GetComponent<EnemyManager>();

            enemyManager.TakeDamage(manager.GetStats().CurrentDamage);
        }
        base.OnTriggerEnter2D(other);
    }
}

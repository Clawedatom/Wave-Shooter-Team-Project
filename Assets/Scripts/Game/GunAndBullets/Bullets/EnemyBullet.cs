using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BaseBullet
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.Instance.TakeDamage(manager.GetStats().CurrentDamage);
        }

        base.OnTriggerEnter2D(other);
    }
}

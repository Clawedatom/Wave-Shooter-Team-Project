using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : BaseBullet
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<BaseManager>().TakeDamage(manager.GetStats().CurrentDamage);
        }

        base.OnTriggerEnter2D(other);
    }
}

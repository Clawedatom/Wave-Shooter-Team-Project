using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    protected BaseStats baseStats;

    public bool IsDead
    {
        get { return baseStats.IsDead; }
        set { baseStats.IsDead = value; }
    }

    public virtual void OnAwake()
    {
        baseStats = GetComponent<BaseStats>();
        baseStats.OnAwake();
    }

    public virtual void OnStart()
    {
        baseStats.OnStart();
    }

    public virtual void OnUpdate()
    {
        baseStats.OnUpdate();
    }



    public virtual void TakeDamage(float amount)
    {
        baseStats.LoseHealth(amount);
    }

    public virtual void HandleDeath()
    {

    }

    public virtual BaseStats GetStats()
    {
        return baseStats;
    }
}

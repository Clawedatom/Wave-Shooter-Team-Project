using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : BaseStats
{
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float timeSinceAttack;
    [SerializeField] public bool canAttack;


    public override void OnAwake()
    {
        base.OnAwake();
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        timeSinceAttack += Time.deltaTime;

        if (timeSinceAttack >= attackSpeed)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }
    public override void LoseHealth(float amount)
    {
        base.LoseHealth(amount);

        //UI.UpdateSLider();
    }


    public void ProcessAttack()
    {
        timeSinceAttack = 0;

    }
}

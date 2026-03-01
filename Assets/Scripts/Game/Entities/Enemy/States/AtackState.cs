using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    EnemyManager enemyManager;

    public EnemyStats enemyStats;
    public override void Enter()
    {

    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            
            if (enemyStats.canAttack == true)
            {
                enemyManager.HandleAttack();
            }
        }
        
    }
}

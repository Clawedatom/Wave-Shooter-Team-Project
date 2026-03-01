using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //private StateMachine stateMachine;
    private NavMeshAgent agent;

    EnemyManager enemyManager;

    [SerializeField] Transform target;//player location

    [SerializeField] private bool showRange;

    public void OnAwake()
    {
        
        enemyManager = GetComponent<EnemyManager>();
    }

    public void OnStart()
    {
        target = PlayerManager.Instance.transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void OnUpdate()
    {
        agent.SetDestination(target.position);
        if((gameObject.tag == "Enemy1" || gameObject.tag == "Enemy2"))
        {
            agent.stoppingDistance = 4;
        }
        
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        //Debug.Log(agent.remainingDistance);

        if ((agent.remainingDistance <= 8 && agent.remainingDistance >=0) && (gameObject.tag == "Enemy1" || gameObject.tag == "Enemy2"))
        {
            enemyManager.HandleRangedAttack();  
        }
    }
   public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && (gameObject.tag == "Enemy" || gameObject.tag == "Enemy2"))
        {
            enemyManager.HandleAttack();    
        }
    }

}

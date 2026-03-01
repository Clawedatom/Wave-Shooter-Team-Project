using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class EnemyManager : BaseManager   
{
    #region Class References
   

    PlayerManager playerManager;
   
    EnemyAI enemyAI;
    public EnemyStats enemyStats;
    EnemyShooting enemyShooting;
    SpriteRenderer sr;

    [SerializeField]EnemySpin enemySpin;

    #endregion

    #region Private Fields
    [Header("Enemy Bools")]

    [SerializeField] private bool isRanged = false;

    [SerializeField] private Color fullHPCol;
    [SerializeField] private Color noHPCol;
    

    #endregion

    #region Properties

    #endregion

        #region Start Up 
    public override void OnAwake()
    {
        base.OnAwake();
        AssignClasses();
        AwakenClasses();
        

    }

    private void AssignClasses()
    {
        playerManager = PlayerManager.Instance;

        enemyAI = GetComponent<EnemyAI>();
        enemyStats = GetComponent<EnemyStats>();
        if (isRanged)
        {
            enemyShooting = GetComponent<EnemyShooting>();

        }
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void AwakenClasses()
    {
        
        enemyStats.OnAwake();
        enemyAI.OnAwake();
    }


    public override void OnStart()
    {
        base.OnStart();
        StartClasses();
        IsDead = false;
    }
    private void StartClasses()
    {
       
        enemyStats.OnStart();
        enemyAI.OnStart();
    }
    #endregion

    #region Update Functions
    public override void OnUpdate()
    {
        base.OnUpdate();        
        enemyStats.OnUpdate();
        enemyAI?.OnUpdate();
        if (enemySpin != null)
        {
            enemySpin.OnUpdate();
        }

        if (enemyShooting != null)
        {

            enemyShooting.OnUpdate();
        }
        if (enemyStats.CurrentHealth == 0)
        {
            IsDead = true;
        }
        
    }
    #endregion

    #region Class Functions
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);


        float normHealth = enemyStats.CurrentHealth / enemyStats.MaxHealth;

        Color newCol = Color.Lerp(noHPCol, fullHPCol, normHealth);

        sr.color = newCol;
    }

    #endregion

    #region Attack Functions
    public void HandleAttack()
    {
        if (enemyStats.canAttack)
        {
            playerManager.TakeDamage(enemyStats.CurrentDamage);
            enemyStats.ProcessAttack();
        }
    }
    public void HandleRangedAttack()
    {
        if (enemyStats.canAttack && (this.tag == "Enemy1" || this.tag == "Enemy2"))
        {
            RangeAttack();
            //playerManager.TakeDamage(enemyStats.CurrentDamage);
            enemyStats.ProcessAttack();
        }
    }


    public void RangeAttack()
    {
        //var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        //bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;

        //var bullet1 = Instantiate(bulletPrefab, bulletSpawnPoint1.position, bulletSpawnPoint1.rotation);
        //bullet1.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint1.up * bulletSpeed;

        //var bullet2 = Instantiate(bulletPrefab, bulletSpawnPoint2.position, bulletSpawnPoint2.rotation);
        //bullet2.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint2.up * bulletSpeed;

        enemyShooting.Gun_Shoot(this);
    }
        #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : BaseManager
{
    #region Class References
    TurretStats turretStats;
    #endregion

    #region Private Fields
    [Header("Bullet Fields")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<TurretBullet> bullets = new List<TurretBullet>();
    
    [Header("Detect Radius Fields")]
    [SerializeField] private GameObject turretNozzleGO;
    [SerializeField] private float turretRange = 10f;
    [SerializeField] private GameObject currentTarget;

    [SerializeField] private LayerMask enemyLayer;


    [Header("Turret Fields")]
    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private GameObject firePoint;

    
    [SerializeField] private float bulletSpeed = 3f;
    [SerializeField] private float fireRate = 1f;
    private float timeSinceFire;
    #endregion

    #region Properties
    
    #endregion

    #region Start Up
    public override void OnAwake()
    {
        base.OnAwake();
        turretStats = (TurretStats)baseStats;
        turretStats.OnAwake();
    }

    public override void OnStart()
    {
        base.OnStart();

        turretStats.OnStart();
    }
    #endregion

    #region Update Functions
    public override void OnUpdate()
    {
        currentTarget = HandleDetectRadius();

        if (currentTarget != null)
        {
            HandleAim();
            HandleShoot();

        }


        ProcessBullets();
    }

    private void ProcessBullets()
    {
        if (bullets.Count == 0) return;

        List<TurretBullet> tempBullet = new List<TurretBullet>(); // bullets to keep because cant remove them while looping

        foreach(TurretBullet bullet in bullets)
        {
            bool toDestroy = bullet.OnUpdate();
            
            if (!toDestroy)
            {
                tempBullet.Add(bullet);
            }
            else
            {
                Destroy(bullet.gameObject);
            }
        }

        bullets = tempBullet;
    }

    private void HandleAim()
    {
        Vector3 directionToEnemy = currentTarget.transform.position - transform.position; // get direction

        float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;

        Quaternion targetRot = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
    }


    private GameObject HandleDetectRadius()
    {
        Vector2 origin = transform.position;

        Collider2D[] cols = Physics2D.OverlapCircleAll(origin, turretRange, enemyLayer);


       

        if (cols.Length > 0)
        {
            Collider2D closest = cols[0];
            
            for (int i = 1; i < cols.Length; i++)
            {
                if (cols[i] != null) 
                { 
                    float closestDist = Vector2.Distance(transform.position, closest.gameObject.transform.position);
                    float loopColDist = Vector2.Distance(transform.position, cols[i].gameObject.transform.position);

                    if (closestDist > loopColDist) // if current closest enemy is further away from current enemy in loop replace
                    {
                        closest = cols[i];
                    }
                }
            }

            return closest.gameObject;
        }
        return null;
    }

    public void OnDrawGizmos()
    {
        //range show
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, turretRange);
    }

    private void HandleShoot()
    {
        timeSinceFire += Time.deltaTime;

        if (timeSinceFire >= fireRate) // if can fire 
        {
            //fire
            timeSinceFire = 0;

            //create bullet
            TurretBullet newBullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity).GetComponent<TurretBullet>();
           
            SetUpBullet(newBullet);

        }
    }

    private void SetUpBullet(TurretBullet bullet)
    {
        bullets.Add(bullet);

        bullet.OnCreate(this, bulletSpeed *turretNozzleGO.transform.up);
    }
    #endregion

    #region Class Functions
    public override BaseStats GetStats()
    {
        return turretStats;
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{

    [SerializeField] private List<BaseBullet> bullets = new List<BaseBullet>();
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    public void HandleShoot(BaseManager manager)//makes the bullet
    {

        BaseBullet bullet = Instantiate(bulletPrefab,bulletSpawnPoint.position,bulletSpawnPoint.rotation).GetComponent<BaseBullet>();
        
       
        SetUpBullet(manager, bullet);
        
        
       
    }

    private void SetUpBullet(BaseManager manager, BaseBullet bullet)
    {
        bullets.Add(bullet);

        bullet.OnCreate(manager,bulletSpawnPoint.up * bulletSpeed);

    }

    public void OnUpdate()
    {
        List<BaseBullet> bulletsToKeep = new List<BaseBullet>();


        foreach(BaseBullet bullet in bullets)
        {
            bool isDead = bullet.OnUpdate();
        
            if (isDead)// removes bullets that are dead from the list
            {
                Destroy(bullet.gameObject);
            }
            else
            {
                bulletsToKeep.Add(bullet);
            }
        }
        bullets = bulletsToKeep;
    }

}

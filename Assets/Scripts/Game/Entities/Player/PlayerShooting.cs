using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    #region Class References
    PlayerManager playerManager;
    GunManager gun;
    #endregion

    #region Private Fields
    [SerializeField] private float fireRate =2;
    [SerializeField] private float timeSinceFire;
    [SerializeField] private bool canShoot;
    #endregion

    #region Properties
    
    #endregion

    #region Start Up
    public void OnAwake()
    {
        gun = FindAnyObjectByType<GunManager>();
        playerManager = PlayerManager.Instance;
    }

    public void OnStart()
    {

    }
    #endregion

    #region Update Functions
    public void OnUpdate(bool shoot)
    {
        gun.OnUpdate();
        if (shoot && canShoot)
        {
            //shoot
            gun.HandleShoot(playerManager);
            timeSinceFire = 0;
            canShoot = false;
        }

        timeSinceFire += Time.deltaTime;

        if (timeSinceFire >= fireRate)
        {
            canShoot = true;
        }
    }
    #endregion

    #region Class Functions

    #endregion
}

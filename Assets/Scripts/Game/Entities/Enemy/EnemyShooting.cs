using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] List<GunManager> gunManagers = new List<GunManager>();
    public void OnUpdate()
    {
        foreach(GunManager gunManager in gunManagers)
        {
            gunManager.OnUpdate();
        }
    }

    public void Gun_Shoot(BaseManager manager)
    {
        foreach (GunManager gunManager in gunManagers)
        {
            gunManager.HandleShoot(manager);
        }
    }
}

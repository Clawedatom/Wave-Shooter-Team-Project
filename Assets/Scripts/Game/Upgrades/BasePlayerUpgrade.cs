using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerUpgrade : MonoBehaviour, IPlayerUpgrade
{
    public string id;

    public void OnPlayerDamaged(float amount)
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerDeath()
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerInit()
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerShoot()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnPlayerUpdate()
    {
        
    }

    
}

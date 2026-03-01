using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesBootsUpgrade : BasePlayerUpgrade
{
    private void Awake()
    {
        id = "hermes";
    }
    new void OnPlayerInit() 
    {
        PlayerManager.Instance.GetMovement().SetMovementSpeed(PlayerManager.Instance.GetMovement().GetMovementSpeed() + 3.0f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerUpgrade
{
    // Called when the item is first collected
    void OnPlayerInit();

    // Called every frame
    void OnPlayerUpdate();

    // Called when the player shoots
    void OnPlayerShoot();

    // Called when the player is damaged by an enemy
    void OnPlayerDamaged(float amount);

    // Called right before the player dies
    void OnPlayerDeath();
}

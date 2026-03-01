using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Upgrade : ScriptableObject
{
    // The name of the upgrade
    public string displayName;

    // The description of the upgrade
    public string description;

    // The weight of the item in the item pool (default is 1)
    public float weight;

    // Internal string ID of the upgrade
    public string id;

    // Player modifier methods
    public BasePlayerUpgrade playerUpgrade;

    // Bullet modifier methods
    public BaseBulletUpgrade bulletUpgrade;

    public void FetchUpgradeClasses(String name)
    {
        switch (name)
        {
            case "hermes":
                playerUpgrade = new HermesBootsUpgrade();
                break;

            default:
                Debug.Log("didnt do it.");
                return;
        }

        Debug.Log("did it!");
    }
}

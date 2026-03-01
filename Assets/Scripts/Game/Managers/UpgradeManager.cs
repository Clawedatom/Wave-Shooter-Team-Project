using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance;

    [SerializeField] private List<Upgrade> allUpgrades;
    private List<Upgrade> collectedItems;

    public static UpgradeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<UpgradeManager>();
                if (_instance == null)
                {
                    Debug.LogError("UpgradeManager has not been assigned");
                }
            }
            return _instance;
        }
    }

    public List<Upgrade> GetCollectedItems() 
    {
        return collectedItems;
    }

    void Awake()
    {
        ResetUpgrades();
    }

    void ResetUpgrades()
    {
        collectedItems = new List<Upgrade>();
    }

    void CollectUpgrade(Upgrade upgrade)
    {
        collectedItems.Add(upgrade);
        upgrade.FetchUpgradeClasses(upgrade.id);
        Debug.Log(upgrade.id);
        if (upgrade.playerUpgrade != null) upgrade.playerUpgrade.OnPlayerInit();

        Debug.Log("Collected upgrade");
    }

    Upgrade GetRandomItemFromPool()
    {
        return allUpgrades[Random.Range(0, allUpgrades.Count)];
    }

    private void Update()
    {
        foreach (Upgrade upgrade in collectedItems)
        {
            if (upgrade.playerUpgrade != null) upgrade.playerUpgrade.OnPlayerUpdate();
            if (upgrade.bulletUpgrade != null) upgrade.bulletUpgrade.OnBulletUpdate();
        }

        // DEBUG
        if (Input.GetKeyDown(KeyCode.U))
        {
            CollectUpgrade(allUpgrades[0]);
        }
    }
}

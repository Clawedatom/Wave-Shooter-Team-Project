using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager
{
    #region Class References
    private static PlayerManager _instance;

    InputManager inputManager;
    PlayerMovement playerMovement;
    PlayerStats playerStats;
    PlayerShooting playerShooting;
    PlayerUI playerUI;
    GameManager gameManager;

    //BuildingManager buildingManager;
    #endregion

    #region Private Fields
    [Header("Player Bools")]
    

    [SerializeField] private bool isShooting;
    [SerializeField] private int turretCount;
    public int TurretCount => turretCount;
    #endregion

    #region Properties
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<PlayerManager>();
                if (_instance == null)
                {
                    Debug.LogError("PlayerManager has not been assigned");
                }
            }
            return _instance;
        }
    }

  
    
    #endregion

    #region Start Up 
    public override void OnAwake()
    {
        AssignClasses();
        base.OnAwake();
        AwakenClasses();

        gameManager = GameManager.Instance;

        playerStats = (PlayerStats)baseStats;
        //QCn: Public void that interacts with the PlayerMovement Script

    }

    private void AssignClasses()
    {
        inputManager = InputManager.Instance;
        //buildingManager = BuildingManager.Instance;

        playerMovement = GetComponent<PlayerMovement>();
        
        playerShooting = GetComponent<PlayerShooting>();
        playerUI = PlayerUI.Instance;

    }

    private void AwakenClasses()
    {
        playerMovement.OnAwake();
        
        playerShooting.OnAwake();
    }


    public override void OnStart()
    {
        base.OnStart();
        StartClasses();
    }
    private void StartClasses()
    {
        playerMovement.OnStart();

        playerShooting.OnStart();
    }
    #endregion

    #region Update Functions
    public override void OnUpdate()
    {
        base.OnUpdate();
        playerMovement.OnUpdate(inputManager.vertical, inputManager.horizontal);

        playerShooting.OnUpdate(isShooting);
    }
    #endregion

    #region Class Functions
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        foreach (Upgrade upgrade in UpgradeManager.Instance.GetCollectedItems())
        {
            if (upgrade.playerUpgrade != null) upgrade.playerUpgrade.OnPlayerDamaged(amount);
        }
    }

    public void GainLevel()
    {
        playerStats.GainLevel();
        Debug.Log("Wave cleared! Player level:" + GetComponent<PlayerStats>().currentLevel);
    }

    public void ToggleShooting(bool state)
    {
        if (gameManager.BuildMode)
        {
            isShooting = false;
            return;
        }
        isShooting = state;
    }

    public override void HandleDeath()
    {
        base.HandleDeath();
        playerUI.Death_OpenDeathUI();
        //inputManager.DisableMovement();
    }

    public bool GetDeathStatus()
    {
        return playerStats.IsDead;
    }

    public override BaseStats GetStats()
    {
        return playerStats;
    }

    public PlayerMovement GetMovement()
    {
        return playerMovement;
    }

    public void IncreaseTurretCount()
    {
        turretCount++;
        playerUI.BuildUI_NewTurret();
        playerUI.BuildUI_UpdateCount(turretCount);
    }

    public void DecreaseTurretCount()
    {
        turretCount--;
    }
    #endregion

    #region Building Functions
    

   
    #endregion
}

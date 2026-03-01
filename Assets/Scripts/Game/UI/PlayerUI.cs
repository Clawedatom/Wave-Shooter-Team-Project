using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    #region Class Referneces
    PauseSystemUI pausedSystemUI;

    private static PlayerUI _instance;
    HealthBarUI healthBarUI;
    DeathUI deathUI;
    LevelCompleteUI levelCompleteUI;
    BuildingUI buildingUI;


    PlayerStats playerStats;
    #endregion

    #region Private Fields


    #endregion

    #region Properties
    public static PlayerUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerUI>();

                if (_instance == null)
                {
                    Debug.LogError("PlayerUI has not been assigned");
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Start Up
    public void OnAwake()
    {
        healthBarUI = GetComponentInChildren<HealthBarUI>();
        deathUI = GetComponentInChildren<DeathUI>();
        levelCompleteUI = GetComponentInChildren<LevelCompleteUI>();

        pausedSystemUI = GetComponentInChildren<PauseSystemUI>();
        buildingUI = GetComponentInChildren<BuildingUI>();


        healthBarUI.OnAwake();
        deathUI.OnAwake();     
        levelCompleteUI.OnAwake();
        pausedSystemUI.OnAwake();
        buildingUI.OnAwake();
    }
    public void OnStart()
    {
        healthBarUI.OnStart();
        deathUI.OnStart();
        pausedSystemUI.OnStart();
        levelCompleteUI.OnStart();
        buildingUI.OnStart();

        playerStats = (PlayerStats)PlayerManager.Instance.GetStats();

        healthBarUI.MaxHealthSet(playerStats.MaxHealth);
    }
    #endregion

    #region Class Functions
    public void OnUpdate()
    {
        healthBarUI.OnUpdate(playerStats.CurrentHealth);
        buildingUI.OnUpdate();
        
    }
    #endregion

    #region Pause UI
    public void HandleEnablePauseUI()
    {
        pausedSystemUI.OpenPauseUI();
    }
    public void HandleDisablePauseUI()
    {
        pausedSystemUI.ClosePauseUI();
    }
    #endregion

    #region Death UI
    public void Death_OpenDeathUI()
    {
        deathUI.ShowDeadScreen();
    }
    #endregion

    #region Level UI
    public void LevelComplete_HandleLevelComplete()
    {
        levelCompleteUI.HandleLevelComplete();
    }
    #endregion

    #region BuildingUI
    public void HandleToggleBuildingUI(bool state)
    {
        if (state) // build mode
        {
            buildingUI.EnableBuildingUI();
        }
        else
        {
            buildingUI.DisableBuildingUI();
        }
    }

    public void BuildUI_NoTurrets()
    {
        buildingUI.NoTurretsText();
    }

    public void BuildUI_NewTurret()
    {
        buildingUI.GetTurretText();
    }

    public void BuildUI_UpdateCount(int count)
    {
        buildingUI.UpdateTurretCount(count);
    }
    #endregion





}

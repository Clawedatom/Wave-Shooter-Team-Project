using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Class References
    private static GameManager _instance;

    InputManager inputManager;
    PlayerUI playerUI;
    PlayerManager playerManager;
    BuildingManager buildingManager;

    CameraHandler cameraHandler;

    EnemySpawner enemySpawner;
    #endregion

    #region Private Fields
    [SerializeField] private bool _isPaused;
    [SerializeField] private bool buildMode;
    public bool BuildMode => buildMode;
    #endregion

    #region Properties
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
                if (_instance == null)
                {
                    Debug.LogError("GameManager has not been assigned");
                }
            }
            return _instance;
        }
    }
    // QCnote: For making sure the script is enabled and if not then it shows an error message

    public bool IsPaused
    {
        get { return _isPaused; }
    }
    #endregion

    #region Start Up
    private void Awake()
    {
        AssignClasses();
        AwakenClasses();
    }

    private void AssignClasses()
    {
        inputManager = InputManager.Instance;
        playerManager = PlayerManager.Instance;
        playerUI = PlayerUI.Instance; //needs to access player stats through playerManager so its processed last
        buildingManager = BuildingManager.Instance;

        cameraHandler = CameraHandler.Instance;

        enemySpawner = GetComponent<EnemySpawner>();
    }

    private void AwakenClasses()
    {
        inputManager.OnAwake();
        playerManager.OnAwake();
        playerUI.OnAwake();

        buildingManager.OnAwake();
        cameraHandler.OnAwake();


        enemySpawner.OnAwake();
    }

    private void Start()
    {
        StartClasses();
    }

    private void StartClasses()
    {
        inputManager.OnStart();
        playerManager.OnStart();
        playerUI.OnStart();

        buildingManager.OnStart();
        cameraHandler.OnStart();
        enemySpawner.OnStart();

        Time.timeScale = 1.0f;
    }
    #endregion

    #region Update Functions
    private void Update()
    {
        UpdateClasses();
    }

    private void UpdateClasses()
    {
        inputManager.OnUpdate();
        
        if (!IsPaused && !playerManager.GetDeathStatus())
        {
            playerManager.OnUpdate();
            playerUI.OnUpdate();

            buildingManager.OnUpdate(buildMode);

            enemySpawner.OnUpdate();
        }
        cameraHandler.OnUpdate();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();
    }

    private void OnFixedUpdate()
    {
        enemySpawner.OnFixedUpdate();
    }
    #endregion

    #region Class Functions

    public void CompleteLevel()
    {
        HandlePauseToggle(false);
        playerUI.LevelComplete_HandleLevelComplete();
    }


    public void HandlePauseToggle(bool openUI)
    {
        if (buildMode)
        {
            ToggleBuildMode(); // turn off first
             
        }
        _isPaused = !IsPaused;
        
      

        if (IsPaused)
        {
            Time.timeScale = 0.0f;
            if (openUI)
            {
                playerUI.HandleEnablePauseUI();
            }
        }
        else if (!IsPaused)
        {
            Time.timeScale = 1.0f;
            playerUI.HandleDisablePauseUI();
        }

    }
    public void HandleAttemptBuild()
    {
        
        if (buildMode && playerManager.TurretCount > 0)
        {
            buildingManager.PlaceBuilding();
            
            playerManager.DecreaseTurretCount();
            playerUI.BuildUI_UpdateCount(playerManager.TurretCount);
        }
        if (buildMode && playerManager.TurretCount == 0)
        {
            //cant build
            playerUI.BuildUI_NoTurrets();
        }
    }

    public void ToggleBuildMode()
    {
        buildMode = !buildMode;
        
        playerUI.HandleToggleBuildingUI(buildMode);
    }
    #endregion
}

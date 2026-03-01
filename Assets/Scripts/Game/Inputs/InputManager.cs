using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Class References
    private static InputManager _instance;

    PlayerControls playerControls;
    PlayerManager playerManager;
    GameManager gameManager;
    #endregion

    #region Private Fields
    [Header("Input Fields")]
    // variables to store values from input action map
    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector2 mouseInput;
    //variables that will hold values to be accessed by other scripts
    public float horizontal;
    public float vertical;
    public float mouseX;
    public float mouseY;

   
    #endregion

    #region Properties
    public static InputManager Instance // Creates a instance of this script where it ensures only 1 of these exists, i usually use these on "Manager" scripts that would be incharge of something big/ manages other scripts
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<InputManager>();
                if (_instance == null)
                {
                    Debug.LogError("InputManager has not been assigned");
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Start Up
    public void OnAwake()
    {
        gameManager = GameManager.Instance;
        playerManager = PlayerManager.Instance;
    }

    public void OnStart()
    {

    }
    #endregion

    #region Update Functions
    public void OnUpdate()
    {
        TickInput();
    }

    private void TickInput() // assigns variables that are used by other scripts
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;

        mouseX = mouseInput.x;
        mouseY = mouseInput.y;
    }
    #endregion

    #region Class Functions
    public void OnEnable() // setting up the inputs
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += playerControls => movementInput = playerControls.ReadValue<Vector2>();
            playerControls.PlayerCombat.Aim.performed += playerControls => mouseInput = playerControls.ReadValue<Vector2>();
            playerControls.PlayerCombat.Fire.started += playerControls => playerManager.ToggleShooting(true);
            playerControls.PlayerCombat.Fire.canceled += playerControls => playerManager.ToggleShooting(false);

            playerControls.PlayerBuilding.PlaceBuilding.performed += playerControls => gameManager.HandleAttemptBuild();
            playerControls.PlayerBuilding.BuildMode.performed += playerControls => gameManager.ToggleBuildMode();

            playerControls.PlayerUI.Pause.performed += playerControls => gameManager.HandlePauseToggle(true);
        }
        playerControls.Enable();
    
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }
    #endregion
}

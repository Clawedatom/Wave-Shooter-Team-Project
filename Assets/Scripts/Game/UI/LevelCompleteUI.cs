using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteUI : MonoBehaviour
{
    #region Class References
    LevelManager levelManager;
    #endregion

    #region Private Fields
    [SerializeField] private GameObject screenGO;

    [SerializeField] private int nextLevel;
    #endregion

    #region Properties
    
    #endregion

    #region Start Up
    public void OnAwake()
    {
        levelManager = LevelManager.Instance;
    }

   
    public void OnStart()
    {
        DisableUI();
    }

   
    #endregion

    #region Update Functions
    public void OnUpdate()
    {
      
    }

   
    #endregion

    #region Class Functions
    public void HandleLevelComplete()
    {
        EnableUI();
    }

    private void EnableUI()
    {
        screenGO.SetActive(true);
    }

    private void DisableUI()
    {
        screenGO?.SetActive(false);
    }

    public void LoadNextLevel()
    {
        levelManager.LoadLevel(nextLevel);
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    #region Class References
    private static GameManager _instance;
    #endregion

    #region Private Fields
    [SerializeField] private GameObject buildingScreen;
    [SerializeField] private TMP_Text turretCountText;

    [SerializeField] private GameObject noTurretsText;
    [SerializeField] private GameObject getTurretText;
    #endregion

    #region Properties
    
    #endregion

    #region Start Up
    public void OnAwake()
    {
    }

    

    public void OnStart()
    {
        DisableBuildingUI();   
        noTurretsText.SetActive(false);
        getTurretText.SetActive(false);

        UpdateTurretCount(0);
    }

    
    #endregion

    #region Update Functions
    public void OnUpdate()
    {
    }

    
    #endregion

    #region Class Functions

    public void UpdateTurretCount(int count)
    {
        turretCountText.text = count.ToString() + "x";
    }
    public void EnableBuildingUI()
    {
        buildingScreen.SetActive(true);
    }

    public void DisableBuildingUI()
    {
        buildingScreen.SetActive(false);
    }

    public void NoTurretsText()
    {
        noTurretsText.SetActive(true);
        StartCoroutine(ShowCantPlaceTurretText());
    }

    public void GetTurretText()
    {
        getTurretText.SetActive(true);
        StartCoroutine(ShowGetTurretText());
    }

    private IEnumerator ShowCantPlaceTurretText()
    {
        yield return new WaitForSeconds(2);
        noTurretsText.SetActive(false);
    }
    private IEnumerator ShowGetTurretText()
    {
        yield return new WaitForSeconds(2);
        getTurretText.SetActive(false);
    }
    #endregion
}

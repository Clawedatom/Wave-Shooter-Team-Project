using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour
{



    #region Class References

    #endregion

    #region Private Fields
    [SerializeField] private Slider HealthSlider;
    #endregion

    #region Properties

    #endregion

    #region Start Up
    public void OnAwake()
    {

    }

    public void OnStart()
    {

    }
    #endregion

    #region Update Functions
    public void OnUpdate(float player_CurrentHealth)
    {
        HealthSet(player_CurrentHealth);
    }
    #endregion

    #region Class Functions
    public void MaxHealthSet(float MaxHealth)
    {
        HealthSlider.maxValue = MaxHealth;
    }
    public void HealthSet(float CurrentHealth)
    {
        HealthSlider.value = CurrentHealth;
    }
    #endregion
}

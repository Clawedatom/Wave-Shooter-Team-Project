using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : BaseStats
{
    #region Class References
    PlayerManager playerManager;
    DeathUI deathUI;
    #endregion

    #region Private Fields
    [Header("Player Stats")]
    //Health regen after being in combat
    [SerializeField] private float healthRegenAmount = 0.5f;
    [SerializeField] private float healthRegenTimer;
    [SerializeField] private float healthRegenCooldown = 3f;
    [SerializeField] public int currentLevel = 1;


    #endregion

    #region Properties
    
    #endregion

    #region Start Up
    public override void OnAwake()
    {
        base.OnAwake();
        playerManager = PlayerManager.Instance;
        
    }
    public override void OnStart()
    {
        base.OnStart();

        IsDead = false;

    }
    #endregion

    #region Update Functions
    public override void OnUpdate()
    {
        base.OnUpdate();
        RegenHealthCheck();
    }
    #endregion

    #region Class Functions
    public override void LoseHealth(float amount)
    {

        base.LoseHealth(amount);

        //UI.UpdateSLider();

        healthRegenTimer = 0f; // reset timer for healing

        if (CurrentHealth == 0)
        {
            IsDead = true;
            playerManager.HandleDeath();
            //show death screen
        }
    }

    public override void GainHealth(float amount)
    {
        base.GainHealth(amount);

        //UI.UpdateSLider();
    }

    public void GainLevel()
    {
        currentLevel++;

        // Call the upgrade interface here
        //
        //
        //
    }

    private void RegenHealthCheck() // only heal if not been hit after "healthRegenCooldown"
    {
        if (CurrentHealth == MaxHealth) return; // dont check if already full hp
        healthRegenTimer += Time.deltaTime; // add to timer

        if (healthRegenTimer >= healthRegenCooldown) // check if timer has done
        {
            GainHealth(healthRegenAmount); // heal player
        }
    }
    #endregion
}

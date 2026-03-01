using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour //stats enemy and player will inherit from
{
    #region Class References

    #endregion

    #region Private Fields
    [Header("Base Stats")]
    [SerializeField] public float _currentHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentDamage;
    [SerializeField] private float _maxDamage;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private bool isDead;

    #endregion

    #region Properties
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            if (_currentHealth > MaxHealth) //ensures current health doesnt go over max health
            {
                _currentHealth = MaxHealth;
            }
            if (_currentHealth <= 0)
            {
                //death
                _currentHealth = 0;
            }
            
        }
    }

    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public float CurrentDamage
    {
        get { return _currentDamage; }
        set { _currentDamage = value; }
    }

    public float MaxDamage
    {
        get { return _maxDamage; }
        set { _maxDamage = value; }
    }

    public float CurrentSpeed
    {
        get { return _currentSpeed; }
        set { _currentSpeed = value; }
    }

    public float MaxSpeed
    {
        get { return _maxSpeed; }
        set { _maxSpeed = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    #endregion

    #region Start Up Functions
    public virtual void OnAwake()
    {

    }

    public virtual void OnStart()
    {
        SetStats();
    }
    #endregion

    #region  Update Functions
    public virtual void OnUpdate()
    {

    }
    #endregion


    #region Class Functions
    public void SetStats()
    {
        CurrentHealth = MaxHealth;
        CurrentDamage = MaxDamage;
        CurrentSpeed = MaxSpeed;
    }

    public virtual void LoseHealth(float amount)
    {
        CurrentHealth -= amount;  

        if (CurrentHealth == 0)
        {
            //dead
            isDead = true;
        }
    }

    public virtual void GainHealth(float amount)
    {
        CurrentHealth += amount;
    }

   
    #endregion
}

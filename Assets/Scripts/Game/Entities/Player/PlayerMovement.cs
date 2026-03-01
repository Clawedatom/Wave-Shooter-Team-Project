using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Class References
    Rigidbody2D rb;
    #endregion

    #region Private Fields
    [Header("Movement Fields")]
    [SerializeField] private Vector2 movementVector;
    [SerializeField] private float movementSpeed = 10f;

    [Header("Aim Fields")]
    [SerializeField] private Vector2 aimVector;
    
    #endregion

    #region Properties
    
    #endregion

    #region Start Up
    public void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();
      
    }

    public void OnStart()
    {
     
    }
    #endregion

    #region Update Functions
    public void OnUpdate(float vertical, float horizontal)
    {
        HandleMovement(vertical, horizontal);

        HandleAim();
        //calls the aim functions and gives parameters for handle movement
    }

    private void HandleAim()
    {
            // Get mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // Calculate direction from player to mouse
            Vector3 direction = mousePosition - transform.position;

            // Calculate the angle and apply rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
    }



    private void HandleMovement(float vertical, float horizontal)
    {
        //multiply input by movement speed and time between frames for constant speed

        movementVector = new Vector2(horizontal, vertical).normalized * movementSpeed;

        rb.linearVelocity = movementVector;

        
    }
    #endregion

    #region Class Functions
    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }

    #endregion
}

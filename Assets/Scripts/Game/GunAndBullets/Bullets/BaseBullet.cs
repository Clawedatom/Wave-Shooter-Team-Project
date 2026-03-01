using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    Rigidbody2D rb;
    protected BaseManager manager;
     
    public float maxTimeAlive = 3;
    public float timeAlive;

    public void OnCreate(BaseManager manager, Vector2 vel)//gets the bullet velocity and gives it a rigibody for collision
    {
        this.manager = manager;
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = vel;
    }
    
    public bool OnUpdate()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive > maxTimeAlive) // counts how long the bullet has been active and compares it to the max time
        {
            return true;
        }

        return false;
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
   
        DestroyBullet();

    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBullet(); //collision for when it hits walls
    }

    protected void DestroyBullet()
    {
        timeAlive = maxTimeAlive; // kill the bullet when time alive reaches max time
        
    }

}

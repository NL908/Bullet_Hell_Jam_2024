using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DroneEnemey : Enemy
{
    [Header("Drone Movement")]
    [SerializeField]
    private float sinSpeed = 1f;
    [SerializeField]
    private float sinDegree = 45f;

    private float randomSeed = 0f;

    protected override void Start()
    {
        base.Start();
        //randomSeed = Random.value * 2 * Mathf.PI;
    }

    // Drone should explode themselves when they hit the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            // Explode, kaboom, deal damage to player
            Player.instance.OnHit(collision.transform.position - transform.position);
            OnDeath(collision.transform.position - transform.position);
        }
    }

    protected override void OnDeath(Vector2 damageDirection)
    {
        AudioManager.instance.PlaySound("DroneDeath");
        base.OnDeath(damageDirection);
    }

    protected override Vector2 CalcSteering(Vector2 target)
    {
        /* Drone is using steering seek behaviour.
           Always charge towards player 
        */
        Vector2 desiredVelocity = (target - (Vector2)transform.position).normalized * maxSpeed;
        // Add a swing pattern to its movement
        desiredVelocity = (Vector2)(Quaternion.Euler(0, 0, sinDegree * Mathf.Sin(Time.time * sinSpeed + randomSeed)) * (Vector3)desiredVelocity);
        Vector2 steering = (desiredVelocity - _rb.velocity);
        
        return steering;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShooterEnemy : Enemy
{
    [Header("Shooter Enemy Variables")]
    [SerializeField]
    private float distanceFromPlayer = 4f;

    protected override Vector2 CalcSteering(Vector2 target)
    {
        /* Shooter enemy uses seek & disengage behaviours
         * Seek when player is far and disengage when player is within range
         */
        Vector2 desiredVelocity = (target - (Vector2)transform.position).normalized * maxSpeed;
        // Moveaway from the player if player is too close and within arena size
        if (Vector2.Distance(target, transform.position) <= distanceFromPlayer && isWithinArena()) 
        {
            desiredVelocity = -desiredVelocity;
        }

        Vector2 steering = (desiredVelocity - _rb.velocity);
        return steering;
    }

    protected override void OnDeath(Vector2 damageDirection)
    {
        AudioManager.instance.PlaySound("ShooterDeath");
        base.OnDeath(damageDirection);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankEnemy: Enemy
{
    [Header("Tank Enemy Variables")]
    [SerializeField]
    private float distanceFromPlayer = 4f;
    protected override Vector2 CalcSteering(Vector2 target)
    {
        /* Tank enemy uses seek and arrival behaviour.
         * Seek when player is far, arrival when player within range.
         */
        Vector2 desiredVelocity = (target - (Vector2)transform.position).normalized * maxSpeed;
        Vector2 steering;
        // Stop moving if player is close AND within arena
        // TODO: Add is within arena check
        if (Vector2.Distance(target, transform.position) <= distanceFromPlayer)
        {
            steering = -_rb.velocity;
        }
        else
        {
            steering = (desiredVelocity - _rb.velocity);
        }
        return steering;
    }
}

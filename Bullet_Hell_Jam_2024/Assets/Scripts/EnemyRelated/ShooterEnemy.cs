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
        Vector2 desiredVelocity = (target - (Vector2)transform.position).normalized * maxSpeed;
        if (Vector2.Distance(target, transform.position) <= distanceFromPlayer)
        {
            desiredVelocity = -desiredVelocity;
        }

        Vector2 steering = (desiredVelocity - _rb.velocity);
        return steering;
    }
}

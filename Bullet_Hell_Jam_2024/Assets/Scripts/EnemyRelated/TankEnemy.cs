using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TankEnemy: Enemy
{
    [Header("Tank Enemy Variables")]
    [SerializeField]
    private float distanceFromPlayer = 4f;
    [SerializeField] float fanEmitterAngularSpeed = 50f;
    [SerializeField] FanProjectileEmitter fanEmitter;
    // Components
    // This is fan's rotation independent of parent's rotation
    Vector3 fanEmitterAngles;
    Quaternion fanEmitterRotation;

    protected override void Start()
    {
        base.Start();
        fanEmitterAngles = Vector3.zero;
        fanEmitterRotation = fanEmitter.transform.rotation;
    }

    void Update() {
        if (isWithinArena()) {
            fanEmitter.isActive = true;
        }
    }

    /// <summary>
    /// Overriding UpdateRotation to rotate fan emitter independant of parent's rotation
    /// </summary>
    protected override void UpdateRotation()
    {
        if (isAlwaysFacePlayer)
        {
            // Rotation is faced towards the player
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Player.instance.transform.position - transform.position);
            
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _rb.velocity);
        }
        fanEmitterAngles.z += Time.deltaTime * fanEmitterAngularSpeed;
        fanEmitter.transform.rotation = Quaternion.Euler(fanEmitterAngles);
    }

    protected override Vector2 CalcSteering(Vector2 target)
    {
        /* Tank enemy uses seek and arrival behaviour.
         * Seek when player is far, arrival when player within range.
         */
        Vector2 desiredVelocity = (target - (Vector2)transform.position).normalized * maxSpeed;
        Vector2 steering;
        // Stop moving if player is close AND within arena
        if (Vector2.Distance(target, transform.position) <= distanceFromPlayer && isWithinArena())
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

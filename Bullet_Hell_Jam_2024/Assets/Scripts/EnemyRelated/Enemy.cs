using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    // No range, charge behavior
    Drone = 0,
    // Range, flee behavior
    Shooter = 1,
    // Long Range, stationary behavior
    Tank = 2
}

public abstract class Enemy : MonoBehaviour
{
    /* Property */
    // For Movement
    [SerializeField] protected float turnRate;
    [SerializeField] protected float maxSpeed = 2f;
    [SerializeField] protected float acceleration;
    [SerializeField] protected Vector2 direction;

    // Stats
    [SerializeField] protected float hp;
    [SerializeField] protected float maxHp;
    [SerializeField] protected int score;

    // Grouping
    protected EnemyType type;

    // Components
    protected Rigidbody2D _rb;
    protected Collider2D _collider;

    // Behavior
    protected ProjectileEmitter[] _emitters; // GetComponents will generate it in the order they are linked in Inspector

    /* Unity Method */
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _emitters = GetComponents<ProjectileEmitter>();
    }

    protected void FixedUpdate()
    {
        CalcAndUpdateVelocity(Player.instance.transform.position);
    }

    #region Steering Behaviour
    protected abstract Vector2 CalcSteering(Vector2 target);

    protected void CalcAndUpdateVelocity(Vector2 target)
    {
        // Get the steering vector 
        Vector2 steering = CalcSteering(target);
        // Clamp to steering vector so that it does not excede the maximum turn rate/
        steering = steering * turnRate * Time.deltaTime;
        // Clamp the velocity so it's within the max speed and apply an acceleration to its current speed
        // Should be identical to (_rb.velocity+steering).normalized * Mathf.min(_rb.velocity.magnitude + acceleration * Time.deltaTime, maxSpeed)
        Vector2 newVelocity = ClampMagnitude(_rb.velocity + steering,
            _rb.velocity.magnitude + acceleration * Time.deltaTime,
            maxSpeed);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, newVelocity);
        _rb.velocity = newVelocity;
    }

    // Vector2.ClampMagnitude with minLength
    // ClampMagnitude(v, -inf, maxLength) functions the same as Vector.ClampMagnitude(v, maxLength)
    protected Vector2 ClampMagnitude(Vector2 v, float min, float max)
    {
        if (min > max)
            min = max;
        double sm = v.sqrMagnitude;
        if (sm > (double)max * (double)max) return v.normalized * max;
        else if (sm < (double)min * (double)min) return v.normalized * min;
        return v;
    }
    #endregion
}

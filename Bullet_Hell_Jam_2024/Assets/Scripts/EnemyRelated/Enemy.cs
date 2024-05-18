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
    [SerializeField] protected float maxSpeed = 2f;
    [SerializeField] protected float steerForce;
    [SerializeField] protected float acceleration;
    [SerializeField] protected float deceleration;
    [SerializeField] protected bool isAlwaysFacePlayer = false;

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

    private Vector2 _arenaSize;

    /* Unity Method */
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _emitters = GetComponents<ProjectileEmitter>();
        _arenaSize = GameMaster.instance.arenaSize;
    }

    protected void FixedUpdate()
    {
        // Turn on emitters if in arena
        if (isWithinArena()) {
            SetEmitters(true);
        }
        CalcAndUpdateVelocity(Player.instance.transform.position);
        UpdateRotation();
    }

    #region Hit & Death
    // When the enemy is hit by something
    public virtual void OnHit(float damageTaken)
    {
        // HP deduction
        hp = Mathf.Clamp(hp - damageTaken, 0, maxHp);
        if (hp <= 0)
        {
            OnDeath();
        }

        // Play hit sound
        // Play hit animation (if we have one)
    }

    // When the enemy dies
    protected virtual void OnDeath()
    {
        // disable hitbox
        // add score
        // play some dead sound
        // do some cool animation
        // throw it into the void
        Destroy(gameObject);
    }
    #endregion

    #region Locomotion
    #region Steering Behaviour
    protected abstract Vector2 CalcSteering(Vector2 target);

    protected void CalcAndUpdateVelocity(Vector2 target)
    {
        // Get the steering vector 
        Vector2 steering = CalcSteering(target);
        // Clamp to steering vector so that it does not excede the maximum turn rate/
        //steering = steering * turnRate * Time.deltaTime;
        steering = steering * steerForce * Time.deltaTime;
        // Clamp the velocity so it's within the max speed and the minimum speed by deceleration
        Vector2 newVelocity = ClampMagnitude(_rb.velocity + steering,
            Mathf.Max(_rb.velocity.magnitude - deceleration * Time.deltaTime, 0f),
            Mathf.Min(_rb.velocity.magnitude + acceleration * Time.deltaTime, maxSpeed));

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
    protected virtual void UpdateRotation()
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
    }

    protected bool isWithinArena()
    {
        float arenaX = _arenaSize.x - 1;
        float arenaY = _arenaSize.y - 1;
        float x = transform.position.x;
        float y = transform.position.y;

        return (x < arenaX / 2 && x > -arenaX / 2 && y < arenaY / 2 && y > -arenaY / 2);
    }
    #endregion

    #region Emitter Control
    protected void SetEmitters(bool active)
    {
        foreach(ProjectileEmitter emmitter in _emitters)
        {
            emmitter.isActive = active;
        }
    }
    #endregion
}

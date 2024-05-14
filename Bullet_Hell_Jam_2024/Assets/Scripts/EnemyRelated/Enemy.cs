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
    [SerializeField] protected float speed;
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
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _emitters = GetComponents<ProjectileEmitter>();
    }
}

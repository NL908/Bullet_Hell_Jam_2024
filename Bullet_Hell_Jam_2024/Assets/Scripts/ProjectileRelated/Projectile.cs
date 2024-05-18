using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected int damageValue = 10;
    [SerializeField] protected int speed = 10;
    /// <summary>
    /// If true, this projectile comes from an enemy and can hit the player. S
    /// Otherwise, this projectile comes from the player and can hit the enemy
    /// </summary>
    [SerializeField] protected bool isEnemy = true;
    [SerializeField] protected bool destroyOnHit = true;

    // Components
    protected Rigidbody2D _rb;
    protected Collider2D _collider;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    protected void OnBecameInvisible()
    {
        // Since the game is in fixed arena with fixed camera position & angle
        // Destory projectile object when leaves camera
        Destroy(gameObject);
    }

    // Method called when another collider enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnemy && other.tag == "Player") {
            // Enemy bullet hit player
            // Debug.Log("Player take damage ouchie ouch");
            Player player= other.GetComponent<Player>();
            player.OnHit();
            if (destroyOnHit) {
                Destroy(gameObject);
            }
        }

        else if (!isEnemy && other.tag == "Enemy") {
            // Player's bullet hit enemy
            //Debug.Log("*Gasp* The enemy!");
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.OnHit(damageValue);
            if (destroyOnHit) {
                Destroy(gameObject);
            }
        }
    }
}

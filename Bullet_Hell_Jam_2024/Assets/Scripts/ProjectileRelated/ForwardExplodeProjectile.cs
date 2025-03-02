using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardExplodeProjectile : HomingProjectile
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] int explosionDamage = 50;
    [SerializeField] float explosionRadius = 2f;
    
    // Method called when another collider enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnemy && other.tag == "Enemy") {
            Explode();
            if (destroyOnHit) {
                Destroy(gameObject);
            }
        }
    }

    void Explode() {
        ParticleSystem particleInstance = Instantiate(particle, transform.position, transform.rotation);
        AudioManager.instance.PlaySound("RocketExplode");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, _enemyLayer);
        foreach (Collider2D hit in colliders) {
            Enemy enemy = hit.GetComponent<Enemy>();
            enemy.OnHit(explosionDamage, enemy.transform.position - transform.position);
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set the color of the Gizmo
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // Draw a wireframe sphere
    }
}

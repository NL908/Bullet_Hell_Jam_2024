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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, _enemyLayer);
        foreach (Collider2D hit in colliders) {
            Debug.Log("*Gasp* The enemy "+hit.name+" caught in explosion");
            Enemy enemy = hit.GetComponent<Enemy>();
            enemy.OnHit(explosionDamage);
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set the color of the Gizmo
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // Draw a wireframe sphere
    }
}

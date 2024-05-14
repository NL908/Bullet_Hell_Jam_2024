using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardExplodeProjectile : ForwardProjectile
{
    public LayerMask enemyLayer;
    [SerializeField] int explosionDamage = 50;
    [SerializeField] float explosionRadius = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Method called when another collider enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnemy && other.tag == "Enemy") {
            Debug.Log("*Gasp* The enemy!");
            if (destroyOnHit) {
                Explode();
                Destroy(gameObject);
            }
        }
    }

    void Explode() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (Collider2D hit in colliders) {
            Debug.Log("*Gasp* The enemy "+hit.name+" caught in explosion");
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set the color of the Gizmo
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // Draw a wireframe sphere
    }
}

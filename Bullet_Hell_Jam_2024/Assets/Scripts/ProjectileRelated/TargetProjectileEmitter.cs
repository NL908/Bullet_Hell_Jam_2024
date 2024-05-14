using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A projectile emitter that fires projectile towards a given target
/// </summary>
public class TargetProjectileEmitter : ProjectileEmitter
{
    // The range of the random offset to add to the angle
    [SerializeField] float randomOffsetRange = 3f;
    protected Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void EmitProjectile() {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Vector3 direction = target.position - projectile.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Adjust angle by 90 degrees if the sprite is oriented up by default
        angle -= 90f;
        projectile.transform.rotation = Quaternion.AngleAxis(angle+Random.Range(-randomOffsetRange, randomOffsetRange), Vector3.forward);
    }

    public void EmitProjectileTowardsTarget(Transform targetTransform) {
        target = targetTransform;
        EmitProjectile();
    }
}

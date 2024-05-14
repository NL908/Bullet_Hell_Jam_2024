using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// A projectile emitter that fires projectile towards a given target
/// </summary>
public class TargetProjectileEmitter : ProjectileEmitter
{
    // The range of the random offset to add to the angle
    [SerializeField] float randomOffsetRange = 3f;
    Transform playerTransform;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void EmitProjectile() {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Adjust angle by 90 degrees if the sprite is oriented up by default
        angle -= 90f;
        projectile.transform.rotation = Quaternion.AngleAxis(angle+Random.Range(-randomOffsetRange, randomOffsetRange), Vector3.forward);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardProjectileEmitter : ProjectileEmitter
{
    // The range of the random offset to add to the angle
    [SerializeField] float randomOffsetRange = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void EmitProjectile() {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.transform.Rotate(new Vector3(0, 0, Random.Range(-randomOffsetRange, randomOffsetRange)));
    }
}

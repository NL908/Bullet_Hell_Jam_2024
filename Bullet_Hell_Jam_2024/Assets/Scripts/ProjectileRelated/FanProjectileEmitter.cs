using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanProjectileEmitter : ProjectileEmitter
{
    [SerializeField] int numberOfProjectiles = 10;
    [SerializeField] int angleSpread = 180;
    [SerializeField] bool targetPlayer = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void EmitProjectile() {
        
        int offsetCount = angleSpread == 360 ? numberOfProjectiles + 1 : numberOfProjectiles;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            EnemyProjectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            float angleOffset = numberOfProjectiles == 1 ? 0 : (angleSpread / (offsetCount - 1)) * i - angleSpread / 2;
            // :Fix not aiming at player with odd number projectiles and 360 degree
            if (angleSpread == 360 && numberOfProjectiles % 2 == 1) {
                projectile.transform.Rotate(new Vector3(0, 0, 180));
            }
            projectile.transform.Rotate(new Vector3(0, 0, angleOffset));
        }
        return;
    }
}

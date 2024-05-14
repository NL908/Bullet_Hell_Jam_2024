using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FanProjectileEmitter : ProjectileEmitter
{
    [SerializeField] int numberOfProjectiles = 10;
    [SerializeField] int angleSpread = 180;
    [SerializeField] bool targetPlayer = true;
    protected Transform playerTransform;
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
        
        int offsetCount = angleSpread == 360 ? numberOfProjectiles + 1 : numberOfProjectiles;
        Vector3 direction = playerTransform.position - transform.position;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            if (targetPlayer) {
                // Adjust angle by 90 degrees if the sprite is oriented up by default
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle -= 90f;
                projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            float angleOffset = numberOfProjectiles == 1 ? 0 : (angleSpread / (offsetCount - 1)) * i - angleSpread / 2;
            // Fix not aiming at player with odd number projectiles and 360 degree
            if (angleSpread == 360 && numberOfProjectiles % 2 == 1) {
                projectile.transform.Rotate(new Vector3(0, 0, 180));
            }
            projectile.transform.Rotate(new Vector3(0, 0, angleOffset));
        }
        return;
    }
}

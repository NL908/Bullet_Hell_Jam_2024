using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardProjectile : Projectile
{
    
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}

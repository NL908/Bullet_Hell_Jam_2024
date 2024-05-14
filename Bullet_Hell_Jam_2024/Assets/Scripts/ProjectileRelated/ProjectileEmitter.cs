using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileEmitter : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Emit projectile when this method is called
    /// </summary>
    public abstract void EmitProjectile();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileEmitter : MonoBehaviour
{
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] public float emitInterval = 0.5f;
    float timer = 0f;
    [SerializeField] public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = emitInterval;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isActive) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                EmitProjectile();
                timer = emitInterval;
            }
        }
    }

    /// <summary>
    /// Emit projectile when this method is called
    /// </summary>
    public abstract void EmitProjectile();
}

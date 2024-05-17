using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Split into more projectiles after a delay
/// </summary>
public class TimedSplitProjectile : ForwardProjectile
{
    [SerializeField] float splitDelay = 2f;
    [SerializeField] ProjectileEmitter emitter;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Split(splitDelay));
    }

    IEnumerator Split(float delay) {
        yield return new WaitForSeconds(delay);
        emitter.EmitProjectile();
        Destroy(gameObject);
    }
}

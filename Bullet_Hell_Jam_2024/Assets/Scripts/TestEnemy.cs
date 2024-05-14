using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] ProjectileEmitter emitter;
    [SerializeField] TargetProjectileEmitter targetEmitter;
    float timer = 1.0f;
    float projectileInterval = .2f;
    bool timerRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        timer = projectileInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                emitter.EmitProjectile();
                targetEmitter.EmitProjectileTowardsTarget(GameObject.FindGameObjectWithTag("Player").transform);
                timer = projectileInterval;
            }
        }
    }
}

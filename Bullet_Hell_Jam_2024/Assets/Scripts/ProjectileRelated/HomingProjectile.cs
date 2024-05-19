using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    [Header("Homing Projectile Variables")]
    [SerializeField]
    protected LayerMask _enemyLayer;
    [SerializeField]
    private float _enemyDirectionRadius;
    [SerializeField]
    private float _turnRate = 360f;

    protected override void Start()
    {
        base.Start();
        _turnRate *= Mathf.Deg2Rad;
    }

    private void FixedUpdate()
    {
        FindEnemy();
        ForwardMove();
    }

    private void ForwardMove()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void FindEnemy()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _enemyDirectionRadius, Vector2.up, 0, _enemyLayer, 0);
        if (hit)
        {
            Vector3 targetDirection = hit.transform.position - transform.position;
            
            Vector3 aimDirection = Vector3.RotateTowards(transform.up, targetDirection, _turnRate * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, aimDirection);
        }
    }
}

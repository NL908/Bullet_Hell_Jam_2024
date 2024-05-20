using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : Enemy
{
    public float pushForce = 5f;
    protected override Vector2 CalcSteering(Vector2 target)
    {
        if (!isWithinArena())
        {
            return -transform.position;
        }
        if (_rb.velocity.magnitude > 0)
        {
            return -_rb.velocity;
        }
        return Vector2.zero;
    }

    protected override void OnDeath(Vector2 damageDirection)
    {
        GameMaster.instance.DestroyStartEnemey();
        base.OnDeath(damageDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 forceDirection = (transform.position - collision.transform.position).normalized;
            _rb.AddForce(forceDirection * pushForce, ForceMode2D.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : Enemy
{
    protected override Vector2 CalcSteering(Vector2 target)
    {
        return Vector2.zero;
    }

    protected override void OnDeath()
    {
        GameMaster.instance.StartGame();
        Destroy(gameObject);
    }
}

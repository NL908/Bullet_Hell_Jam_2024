using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] protected int damage_value = 10;
    [SerializeField] protected int speed = 10;
    [SerializeField] protected bool destroy_on_hit = true;

    protected void OnBecameInvisible()
    {
        // Since the game is in fixed arena with fixed camera position & angle
        // Destory projectile object when leaves camera
        Destroy(gameObject);
    }

}

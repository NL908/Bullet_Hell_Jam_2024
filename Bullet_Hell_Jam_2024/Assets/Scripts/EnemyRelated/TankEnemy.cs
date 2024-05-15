using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankEnemy: Enemy
{


    /* Implementation of Abtract Method */

    // Handle events on hit
    protected void onHit()
    {

    }

    /* Engine Method */
    private void OnTriggerEnter2D(Collider2D collision)
    {
                
    }
    private void OnBecameInvisible()
    {
        
    }

    protected override Vector2 CalcSteering(Vector2 target)
    {
        throw new System.NotImplementedException();
    }
}

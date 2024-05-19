using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenClearBomb : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyAndProjLayer;

    // Radius per second
    [SerializeField] float _expandingSpeed;
    private float _maxRadius;
    private void Start()
    {

        _maxRadius = GameMaster.instance.arenaSize.magnitude + 4;
    }

    private void FixedUpdate()
    {
        Vector3 scale = transform.localScale;
        scale.x += _expandingSpeed * Time.deltaTime;
        scale.y = scale.x;
        transform.localScale = scale;
    }

    private void LateUpdate()
    {
        // Destroy the bomb when radius reach max
        if (transform.localScale.x > _maxRadius)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((_enemyAndProjLayer.value >> collision.gameObject.layer) & 1) > 0)
        {
            Destroy(collision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float movementSpeed = 5f;

    private Vector2 _arenaSize;
    private Vector2 _objectSize;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _arenaSize = GameMaster.instance.arenaSize;
        _objectSize = GetComponentInChildren<SpriteRenderer>().bounds.size;
    }

    public void HandleMovement(Vector2 moveDirection)
    {
        _rb.velocity = moveDirection * movementSpeed;
        // stop moving if player ded
        if (Player.instance.isDead) _rb.velocity *= 0;
    }

    public void HandleAimDirection(Vector2 aimDirection)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, aimDirection);
    }
    private void LateUpdate()
    {
        ClampPositionWithinBoundaries();
    }

    // Prevent player going outside of the arena
    private void ClampPositionWithinBoundaries()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -(_arenaSize.x / 2 - _objectSize.x / 2), _arenaSize.x / 2 - _objectSize.x / 2);
        pos.y = Mathf.Clamp(pos.y, -(_arenaSize.y / 2 - _objectSize.y / 2), _arenaSize.y / 2 - _objectSize.y / 2);
        transform.position = pos;
    }
}

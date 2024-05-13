using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float movementSpeed = 5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void HandleMovement(Vector2 moveDirection)
    {
        _rb.velocity = moveDirection * movementSpeed;
    }

    public void HandleAimDirection(Vector2 aimDirection)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, aimDirection);
    }
}

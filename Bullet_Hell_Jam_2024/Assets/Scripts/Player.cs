using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    PlayerInputHandler inputHandler;
    PlayerLocomotion playerLocomotion;

    Rigidbody2D _rb;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Gather player input
        inputHandler.TickInput();
    }

    private void FixedUpdate()
    {
        UpdateLocomotion();
    }

    private void UpdateLocomotion()
    {
        playerLocomotion.HandleMovement(inputHandler.movementDirection);
        playerLocomotion.HandleAimDirection(inputHandler.aimDirection);
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            // Draw aim direction with red line
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, inputHandler.aimDirection * 3f);

            // Draw move direction with blue line
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, inputHandler.movementDirection * 3f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerControls inputActions;

    #region Movement variables
    [Header("Movement Related")]
    // Player's movement direction
    public Vector2 movementDirection;

    private InputAction _movementAction;
    #endregion

    #region Aim variables
    [Header("Aim Related")]
    // Mouse screen position input
    public Vector2 mouseAimPositionInput;

    // Controller aim direction input
    public Vector2 controllerAimDirectionInput;
    
    // Player's aiming direction. A normalized Vector2
    public Vector2 aimDirection = new Vector2(1, 0);

    // A flag toggle between mouse aiming and controller aiming
    // True if mouse position movement was detected and false if controller aim detected
    public bool isMouseAimControl = true;

    private InputAction _mouseAimPositionAction, _controllerAimDirectionAction;
    #endregion

    #region Fire variables
    [Header("Fire Related")]
    // The fire button. True if pressed
    public bool fireButton;
    private InputAction _fireAction;
    #endregion

    private void Awake()
    {
        inputActions = new PlayerControls();

        _movementAction = inputActions.Player.Movement;

        _mouseAimPositionAction = inputActions.Player.MouseAimPosition;
        _mouseAimPositionAction.performed += obj => isMouseAimControl = true;

        _controllerAimDirectionAction = inputActions.Player.ControllerAimDirection;
        _controllerAimDirectionAction.performed += obj => isMouseAimControl = false;

        _fireAction = inputActions.Player.Fire;
    }

    private void OnEnable()
    {
        _movementAction.Enable();

        _mouseAimPositionAction.Enable();

        _controllerAimDirectionAction.Enable();

        _fireAction.Enable();
    }

    private void OnDisable()
    {
        _movementAction.Disable();

        _mouseAimPositionAction.Disable();

        _controllerAimDirectionAction.Disable();

        _fireAction.Disable();
    }

    public void TickInput()
    {
        MovementInput();
        AimInput();
        FireButtonInput();
    }

    private void MovementInput()
    {
        movementDirection = _movementAction.ReadValue<Vector2>();
    }

    private void AimInput()
    {
        if(isMouseAimControl)
        {
            mouseAimPositionInput = _mouseAimPositionAction.ReadValue<Vector2>();
        }
        else
        {
            controllerAimDirectionInput = _controllerAimDirectionAction.ReadValue<Vector2>();
        }

        // Calculate aim direction
        aimDirection = isMouseAimControl ? ((Vector2)(Camera.main.ScreenToWorldPoint(mouseAimPositionInput) - transform.position)).normalized
            : (controllerAimDirectionInput == Vector2.zero ? aimDirection : controllerAimDirectionInput);
        aimDirection.Normalize();
    }

    private void FireButtonInput()
    {
        fireButton = _fireAction.IsPressed();
    }

    private void Update()
    {
        TickInput();
    }
}

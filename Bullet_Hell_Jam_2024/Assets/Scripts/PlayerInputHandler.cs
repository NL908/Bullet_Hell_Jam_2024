using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls inputActions;
    private PlayerWeaponManager playerWeaponManager;

    #region Movement variables
    [Header("Movement Related")]
    // Player's movement direction
    public Vector2 movementDirection;

    private InputAction _movementAction;
    #endregion

    #region Aim variables
    [Header("Aim Related")]
    // Mouse screen position input
    [SerializeField] private Vector2 mouseAimPositionInput;

    // Controller aim direction input
    [SerializeField] private Vector2 controllerAimDirectionInput;
    
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

        inputActions.Player.SelectPreviousWeapon.performed += SelectPreviousWeapon_performed;
        inputActions.Player.SelectNextWeapon.performed += SelectNextWeapon_performed;

        inputActions.Player.SelectWeapon1.performed += SelectWeapon1_performed;
        inputActions.Player.SelectWeapon2.performed += SelectWeapon2_performed;
        inputActions.Player.SelectWeapon3.performed += SelectWeapon3_performed;
    }

    private void Start()
    {
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
    }

    private void OnEnable()
    {
        _movementAction.Enable();

        _mouseAimPositionAction.Enable();

        _controllerAimDirectionAction.Enable();

        _fireAction.Enable();

        inputActions.Player.SelectPreviousWeapon.Enable();
        inputActions.Player.SelectNextWeapon.Enable();

        inputActions.Player.SelectWeapon1.Enable();
        inputActions.Player.SelectWeapon2.Enable();
        inputActions.Player.SelectWeapon3.Enable();
    }

    private void OnDisable()
    {
        _movementAction.Disable();

        _mouseAimPositionAction.Disable();

        _controllerAimDirectionAction.Disable();

        _fireAction.Disable();

        inputActions.Player.SelectPreviousWeapon.Disable();
        inputActions.Player.SelectNextWeapon.Disable();

        inputActions.Player.SelectWeapon1.Disable();
        inputActions.Player.SelectWeapon2.Disable();
        inputActions.Player.SelectWeapon3.Disable();
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

    private void SelectPreviousWeapon_performed(InputAction.CallbackContext obj)
    {
        playerWeaponManager.SelectPrevWeapon();
    }
    private void SelectNextWeapon_performed(InputAction.CallbackContext obj)
    {
        playerWeaponManager.SelectNextWeapon();
    }

    private void SelectWeapon1_performed(InputAction.CallbackContext obj)
    {
        playerWeaponManager.SelectWeapon1();
    }

    private void SelectWeapon2_performed(InputAction.CallbackContext obj)
    {
        playerWeaponManager.SelectWeapon2();
    }

    private void SelectWeapon3_performed(InputAction.CallbackContext obj)
    {
        playerWeaponManager.SelectWeapon3();
    }
}

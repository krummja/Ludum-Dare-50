using System;
using UnityEngine;
using UnityEngine.InputSystem;
using LD50;


public struct PlayerCharacterInputs
{
    public Vector2 MoveInput;
}


public class InputManager : BaseManager<InputManager>
{
    [HideInInspector]
    public PlayerCharacterInputs Inputs;

    private MasterInput inputAction;
    private InputAction moveInput;

    protected override void OnAwake()
    {
        Inputs = new PlayerCharacterInputs();

        inputAction = new MasterInput();
        moveInput = inputAction.PlayerController.Movement;

        moveInput.performed += OnMoveInput;
        // TODO Can I add other callbacks here, for sound and such?
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Inputs.MoveInput = context.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }
}

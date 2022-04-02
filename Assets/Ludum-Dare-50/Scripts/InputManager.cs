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
    private bool pauseInput = true;

    protected override void OnAwake()
    {
        Inputs = new PlayerCharacterInputs();

        inputAction = new MasterInput();
        moveInput = inputAction.PlayerController.Movement;

        AddListenersToInput();
        // TODO Can I add other callbacks here, for sound and such?
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Inputs.MoveInput = context.ReadValue<Vector2>();
    }

    private void AddListenersToInput()
    {
        moveInput.performed += OnMoveInput;
    }

    private void RemoveListenersFromInput()
    {
        moveInput.performed -= OnMoveInput;
    }

    private void OnEnable()
    {
        AddListenersToInput();
        inputAction.Enable();
    }

    private void OnDisable()
    {
        RemoveListenersFromInput();
        inputAction.Disable();
    }
}

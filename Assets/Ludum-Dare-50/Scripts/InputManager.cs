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

    private MasterInput masterInput;
    private InputAction moveInput;
    private InputAction downArrow;
    private bool pauseInput = false;

    private Keyboard keyboard = Keyboard.current;

    public void ToggleInput()
    {
        pauseInput = !pauseInput;
    }

    protected override void OnAwake()
    {
        Inputs = new PlayerCharacterInputs();

        masterInput = new MasterInput();
        moveInput = masterInput.PlayerController.Movement;

        AddListenersToInput();
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
        masterInput.Enable();
    }

    private void OnDisable()
    {
        RemoveListenersFromInput();
        masterInput.Disable();
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using LD50;


public struct PlayerCharacterInputs
{
    public Vector2 MoveInput;
}


public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [HideInInspector]
    public PlayerCharacterInputs Inputs;

    private MasterInput inputAction;
    private InputAction moveInput;

    private void Awake()
    {
        if ( Instance != null ) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Inputs = new PlayerCharacterInputs();

        inputAction = new MasterInput();
        moveInput = inputAction.PlayerController.Movement;

        moveInput.performed += OnMoveInput;
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

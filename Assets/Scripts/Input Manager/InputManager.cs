using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private CharacterActions _characterActions;

    private Vector2 _moveInput;
    private Vector2 _viewInput;

    private bool _sprint;
    private bool _jump;

    public Vector2 MoveInput => _moveInput;
    public Vector2 ViewInput => _viewInput;
    public bool Sprint => _sprint;
    public bool Jump => _jump;

    private void Awake()
    {
        _characterActions = new CharacterActions();

        _characterActions.Player.Movement.performed += OnMove;
        _characterActions.Player.View.performed += OnView;

        _characterActions.Player.Sprint.performed += OnSprint;
        _characterActions.Player.Jump.performed += OnJump;

        _characterActions.Player.Movement.canceled += OnMove;
        _characterActions.Player.Sprint.canceled += OnSprint;
        _characterActions.Player.Jump.canceled += OnJump;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnView(InputAction.CallbackContext context)
    {
        _viewInput = context.ReadValue<Vector2>();
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        _sprint = context.ReadValueAsButton();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _jump = context.ReadValueAsButton();
    }


    #region - OnEnbale/OnDisable -

    private void OnEnable()
    {
        _characterActions.Player.Enable();
    }

    private void OnDisable()
    {
        _characterActions.Player.Disable();
    }

    #endregion
}

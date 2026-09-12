using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls Instance { get; private set; }

    public static InputActionAsset inputActions;

    public event Action<Vector2> OnMove;
    public event Action OnInteractPressed;
    public event Action OnJumpPressed;
    public event Action OnJumpReleased;
    public event Action OnPausePressed;
    public event Action OnUnPausedPressed;

    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction jumpAction;
    private InputAction pauseAction;
    private InputAction onUnPausedAction;

    private Action<InputAction.CallbackContext> onMovePerformed;
    private Action<InputAction.CallbackContext> onMoveCancelled;
    private Action<InputAction.CallbackContext> onInteractPerfomed;
    private Action<InputAction.CallbackContext> onJumpPerformed;
    private Action<InputAction.CallbackContext> onJumpCancelled;
    private Action<InputAction.CallbackContext> onPausePerformed;
    private Action<InputAction.CallbackContext> onUnPausedPerformed;


    private void Awake()
    {
        Instance = this;
        inputActions = InputSystem.actions;

        moveAction = InputSystem.actions.FindAction("Move");
        interactAction = InputSystem.actions.FindAction("Interact");
        jumpAction = InputSystem.actions.FindAction("Jump");
        pauseAction = InputSystem.actions.FindAction("PausedButton");

        onUnPausedAction = InputSystem.actions.FindAction("UnpausedButton");

        onMovePerformed = ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        onMoveCancelled = ctx => OnMove?.Invoke(Vector2.zero);

        onJumpPerformed = ctx => OnJumpPressed?.Invoke();
        onJumpCancelled = ctx => OnJumpReleased?.Invoke();

        onInteractPerfomed = ctx => OnInteractPressed?.Invoke();

        onPausePerformed = ctx => OnPausePressed?.Invoke();

        onUnPausedPerformed = ctx => OnUnPausedPressed?.Invoke();
    }

    private void OnEnable()
    {
        moveAction.performed += onMovePerformed;
        moveAction.canceled += onMoveCancelled;
        interactAction.performed += onInteractPerfomed;
        jumpAction.performed += onJumpPerformed;
        jumpAction.canceled += onJumpCancelled;
        pauseAction.performed += onPausePerformed;

        onUnPausedAction.performed += onUnPausedPerformed;

        inputActions.FindActionMap("Player").Enable();
        inputActions.FindActionMap("UI").Disable();
    }

    private void OnDisable()
    {
        moveAction.performed -= onMovePerformed;
        moveAction.canceled -= onMoveCancelled;
        interactAction.performed += onInteractPerfomed;
        jumpAction.performed -= onJumpPerformed;
        jumpAction.canceled -= onJumpCancelled;
        pauseAction.performed -= onPausePerformed;

        onUnPausedAction.performed -= onUnPausedPerformed;

        inputActions.FindActionMap("Player").Disable();
        inputActions.FindActionMap("UI").Disable();
    }

}

using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerStateMachine : MonoBehaviour
{
    [Header("Current State Type")]
    [SerializeField] private StateType _currentStateType;

    [Header("Smoothing Speed Animation Transition")]
    [SerializeField] private float _smoothSpeed;

    private PlayerBaseState _currentState;
    private Dictionary<StateType, PlayerBaseState> _states;

    private PlayerController _controller;
    private Animator _animator;

    public float SmoothSpeed => _smoothSpeed;

    public Animator Animator
    {
        get => _animator;
        set => _animator = value;
    }

    private void Awake()
    {
        SetReferences();
        InitDicStates();
    }

    private void Start()
    {
        foreach(StateType key in _states.Keys)
        {
            if(_states[key] != null)
            {
                _states[key].SetPlayerController(_controller);
                _states[key].SetAnimator(_animator);
            }
        }

        _currentStateType = StateType.Idle;

        if (_states.ContainsKey(_currentStateType))
        {
            _currentState = _states[_currentStateType];
            _currentState.EnterState();
        }

    }

    private void Update()
    {
        if (_currentState == null)
            return;

        StateType newStateType = _currentState.UpdateState();

        if(_currentStateType != newStateType)
        {
            PlayerBaseState newState = null;

            if(_states.TryGetValue(newStateType, out newState))
            {
                _currentState.ExitState();
                newState.EnterState();
                _currentState = newState;
            }
            else if(_states.TryGetValue(StateType.Idle, out newState))
            {
                _currentState.ExitState();
                newState.EnterState();
            }

            _currentStateType = newStateType;
        }
    }

    private void InitDicStates()
    {
        _states = new Dictionary<StateType, PlayerBaseState>();

        PlayerState_Idle idle = new PlayerState_Idle();
        PlayerState_Walking walking = new PlayerState_Walking();
        PlayerState_Sprinting sprinting = new PlayerState_Sprinting();
        PlayerState_JumpingUp jumpingUp = new PlayerState_JumpingUp();

        idle.SetStateMachine(this);
        walking.SetStateMachine(this);
        sprinting.SetStateMachine(this);
        jumpingUp.SetStateMachine(this);

        _states[StateType.Idle] = idle;
        _states[StateType.Walking] = walking;
        _states[StateType.Sprinting] = sprinting;
        _states[StateType.JumpingUp] = jumpingUp;
    }

    private void SetReferences() 
    {
        _controller = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }
}

public enum StateType
{
    Idle,
    Walking,
    Sprinting,
    JumpingUp,
    Falling,
    Crouching,

}

using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [Header("Smooth Animation Transition")]
    [SerializeField] private float _smoothSpeed;

    private PlayerController _playerController;

    private float _horizontalSpeed;
    private float _verticalSpeed;
    private bool _isWalking;
    private bool _isSprinting;
    private bool _isJumping;
    private bool _isFalling;
    private bool _isLanding;


    private int XVelocityHash = Animator.StringToHash("X_Velocity");
    private int YVelocityHash = Animator.StringToHash("Y_Velocity");
    private int IsWalkingHash = Animator.StringToHash("IsWalking");
    private int IsSprintingHash = Animator.StringToHash("IsSprinting");
    private int IsJumpingHash = Animator.StringToHash("IsJumping");
    private int IsFallingHash = Animator.StringToHash("IsFalling");
    private int IsLandingHash = Animator.StringToHash("IsLanding");

    public float SmoothSpeed => _smoothSpeed;
    public float HorizontalSpeed
    {
        get => _horizontalSpeed;
        set => _horizontalSpeed = value;
    }

    public float VerticalSpeed
    {
        get => _verticalSpeed;
        set => _verticalSpeed = value;
    }

    public bool IsSprinting
    {
        get => _isSprinting;
        set => _isSprinting = value;
    }
    public bool IsJumping
    {
        get => _isJumping;
        set => _isJumping = value;
    }

    public bool IsFalling
    {
        get => _isFalling;
        set => _isFalling = value;
    }

    public bool IsLanding
    {
        get => _isLanding;
        set => _isLanding = value;
    }
    public bool IsWalking
    {
        get => _isWalking;
        set => _isWalking = value;
    }

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();

        InitStates();
    }

    private void Start()
    {
        foreach(StateType key in _states.Keys)
        {
            if(_states[key] != null)
            {
                PlayerBaseState state = _states[key] as PlayerBaseState;
                if (state != null)
                {
                    state.PlayerController = _playerController;
                }
            }
        }

        CurrentStateType = StateType.Idle;

        if (_states.ContainsKey(CurrentStateType))
        {
            CurrentState = _states[CurrentStateType];
            CurrentState.EnterState();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (_animator == null)
            return;

        _animator.SetFloat(XVelocityHash, _horizontalSpeed);
        _animator.SetFloat(YVelocityHash, _verticalSpeed);
        _animator.SetBool(IsSprintingHash, _isSprinting);
        _animator.SetBool(IsWalkingHash, _isWalking);

        if (_isJumping)
        {
            _animator.SetTrigger(IsJumpingHash);
            _isJumping = false;
        }

        if (_isFalling)
        {
            _animator.SetTrigger(IsFallingHash);
            _isFalling = false;
        }

        if (_isLanding)
        {
            _animator.SetTrigger(IsLandingHash);
            _isLanding = false;
        }
    }

    protected override void InitStates()
    {
        _states = new Dictionary<StateType, State>();

        PlayerState_Idle idle = new PlayerState_Idle();
        PlayerState_Walking walking = new PlayerState_Walking();
        PlayerState_Sprinting sprinting = new PlayerState_Sprinting();
        PlayerState_JumpingUp jumpingUp = new PlayerState_JumpingUp();
        PlayerState_Falling falling = new PlayerState_Falling();
        PlayerState_Landing landing = new PlayerState_Landing();
        PlayerState_JumpWithWalk jumpWithWalk = new PlayerState_JumpWithWalk();

        idle.SetStateMachine(this);
        walking.SetStateMachine(this);
        sprinting.SetStateMachine(this);
        jumpingUp.SetStateMachine(this);
        falling.SetStateMachine(this);
        landing.SetStateMachine(this);
        jumpWithWalk.SetStateMachine(this);

        _states[StateType.Idle] = idle;
        _states[StateType.Walking] = walking;
        _states[StateType.Sprinting] = sprinting;
        _states[StateType.JumpingUp] = jumpingUp;
        _states[StateType.Falling] = falling;
        _states[StateType.Landing] = landing;
        _states[StateType.JumpingWithWalk] = jumpWithWalk;
    }
}

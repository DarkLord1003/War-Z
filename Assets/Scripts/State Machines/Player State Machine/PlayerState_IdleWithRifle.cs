using UnityEngine;

[System.Serializable]
public class PlayerState_IdleWithRifle : PlayerBaseState
{
    [Header("Right Hand Position And Rotation")]
    [SerializeField] private Vector3 _rightHandPosition;
    [SerializeField] private Vector3 _rightHandRotation;

    [Header("Right Hint Position And Rotation")]
    [SerializeField] private Vector3 _rightHintPosition;
    [SerializeField] private Vector3 _rightHintRotation;

    [Header("Left Hand Position And Rotation")]
    [SerializeField] private Vector3 _leftHandPosition;
    [SerializeField] private Vector3 _leftHandRotation;

    [Header("Left Hint Position And Rotation")]
    [SerializeField] private Vector3 _leftHintPosition;
    [SerializeField] private Vector3 _leftHintRotation;


    public Vector3 RightHandPosition { get; set; }
    public override StateType GetStateType()
    {
        return StateType.IdleWithRifle;
    }

    public override void EnterState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Enter - Idle With Rifle");
        _playerStateMachine.HorizontalSpeed = 0f;
        _playerStateMachine.VerticalSpeed = 0f;
        _playerStateMachine.IsWalking = false;
        _playerStateMachine.IsSprinting = false;
        _playerStateMachine.IsJumping = false;
        _playerStateMachine.IsFalling = false;
        _playerStateMachine.IsLanding = false;

    }

    public override StateType UpdateState()
    {
        if (_playerStateMachine == null || _playerController == null)
            return StateType.None;

        if((Mathf.Abs(_playerController.HorizontalMove) > 0.2f || Mathf.Abs(_playerController.VerticalMove) > 0.2f)
            && _playerController.IsEquippedWeapon)
        {
            return StateType.WalkWithRifle;
        }

        if((Mathf.Abs(_playerController.HorizontalMove) > 0.2f || Mathf.Abs(_playerController.VerticalMove) > 0.2f)
            && !_playerController.IsEquippedWeapon)
        {
            return StateType.Walking;
        }

        if((Mathf.Abs(_playerController.HorizontalMove) < 0.2f && Mathf.Abs(_playerController.VerticalMove) < 0.2f)
            && !_playerController.IsEquippedWeapon)
        {
            return StateType.Idle;
        }

        if (_playerController.IsJumping)
        {
            return StateType.JumpingUp;
        }


        return StateType.IdleWithRifle;
    }

    public override void AnimatorIKUpdate()
    {
    }

    public override void ExitState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Exit - Idle With Rifle");
    }
}

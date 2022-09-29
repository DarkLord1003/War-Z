using UnityEngine;

public class PlayerState_JumpWithWalk : PlayerBaseState
{
    public override StateType GetStateType()
    {
        return StateType.JumpingWithWalk;
    }

    public override void EnterState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Enter - Jumping With Walk");

        _playerStateMachine.HorizontalSpeed = 0f;
        _playerStateMachine.VerticalSpeed = 0f;
        _playerStateMachine.IsFalling = false;
        _playerStateMachine.IsJumping = true;
        _playerStateMachine.IsWalking = false;
        _playerStateMachine.IsSprinting = false;
        _playerStateMachine.IsLanding = false;
    }

    public override StateType UpdateState()
    {
        if (_playerController == null || _playerStateMachine == null)
            return StateType.None;

        //_playerStateMachine.HorizontalSpeed = Mathf.Lerp(_playerStateMachine.HorizontalSpeed,
        //                                                 _playerController.HorizontalMove,
        //                                                 _playerStateMachine.SmoothSpeed * Time.deltaTime);
        
        //_playerStateMachine.VerticalSpeed = Mathf.Lerp(_playerStateMachine.VerticalSpeed,
        //                                                 _playerController.VerticalMove,
        //                                                 _playerStateMachine.SmoothSpeed * Time.deltaTime);

        if (_playerController.IsFalling)
        {
            return StateType.Falling;
        }

        return StateType.JumpingWithWalk;
    }

    public override void ExitState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Exit - Jumping With Walk");

        _playerStateMachine.IsJumping = false;
    }
}

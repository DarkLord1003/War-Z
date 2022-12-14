using UnityEngine;

public class PlayerState_JumpingUp : PlayerBaseState
{
    public override StateType GetStateType()
    {
        return StateType.JumpingUp;
    }

    public override void EnterState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Enter - JumpingUp");

        _playerStateMachine.HorizontalSpeed = 0f;
        _playerStateMachine.VerticalSpeed = 0f;
        _playerStateMachine.IsJumping = true;
        _playerStateMachine.IsWalking = false;
        _playerStateMachine.IsFalling = false;
        _playerStateMachine.IsLanding = false;

    }

    public override StateType UpdateState()
    {
        if (_playerController == null || _playerStateMachine == null)
            return StateType.None;

        if (_playerController.IsFalling)
        {
            return StateType.Falling;
        }

        return StateType.JumpingUp;
    }

    public override void ExitState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("exit - JumpingUp");

        _playerStateMachine.IsJumping = false;
    }
}

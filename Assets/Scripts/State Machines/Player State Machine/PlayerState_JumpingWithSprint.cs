using UnityEngine;

public class PlayerState_JumpingWithSprint : PlayerBaseState
{
    public override StateType GetStateType()
    {
        return StateType.JumpingWithSprint;
    }

    public override void EnterState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Enter - Jumping With Sprint");

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

        if (_playerController.IsFalling)
        {
            return StateType.Falling;
        }

        return StateType.JumpingWithSprint;
    }

    public override void ExitState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Exit - Jumping With Sprint");

        _playerStateMachine.IsJumping = false;
    }
}

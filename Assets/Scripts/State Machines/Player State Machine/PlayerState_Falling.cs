using UnityEngine;

public class PlayerState_Falling : PlayerBaseState
{
    public override StateType GetStateType()
    {
        return StateType.Falling;
    }

    public override void EnterState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Enter - Falling");

        _playerStateMachine.HorizontalSpeed = 0f;
        _playerStateMachine.VerticalSpeed = 0f;
        _playerStateMachine.IsFalling = true;
        _playerStateMachine.IsJumping = false;
        _playerStateMachine.IsWalking = false;
        _playerStateMachine.IsSprinting = false;
        _playerStateMachine.IsLanding = false;
    }

    public override StateType UpdateState()
    {
        if (_playerController == null || _playerStateMachine == null)
            return StateType.None;

        if (_playerController.IsGrounded)
        {
            return StateType.Landing;
        }

        return StateType.Falling;
    }

    public override void ExitState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Exit - Falling");

        _playerStateMachine.IsFalling = false;
    }
}

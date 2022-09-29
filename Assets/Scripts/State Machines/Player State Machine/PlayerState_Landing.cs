using UnityEngine;

public class PlayerState_Landing : PlayerBaseState
{
    public override StateType GetStateType()
    {
        return StateType.Landing;
    }

    public override void EnterState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Enter - Landing");

        _playerStateMachine.HorizontalSpeed = 0f;
        _playerStateMachine.VerticalSpeed = 0f;
        _playerStateMachine.IsLanding = true;
        _playerStateMachine.IsFalling = false;
        _playerStateMachine.IsJumping = false;
        _playerStateMachine.IsWalking = false;
        _playerStateMachine.IsSprinting = false;
    }

    public override StateType UpdateState()
    {
        if (_playerController == null || _playerStateMachine == null)
            return StateType.None;

        if(Mathf.Abs(_playerController.HorizontalMove) < 0.1f && Mathf.Abs(_playerController.VerticalMove) < 0.1f)
        {
            return StateType.Idle;
        }
         
        if(Mathf.Abs(_playerController.HorizontalMove) > 0.2f || Mathf.Abs(_playerController.VerticalMove) > 0.2f)
        {
            return StateType.Walking;
        }




        return StateType.Landing;
    }

    public override void ExitState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Exit - Landing");

        _playerStateMachine.IsLanding = false;
    }
}

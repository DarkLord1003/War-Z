using UnityEngine;

public class PlayerState_Idle : PlayerBaseState
{
    public override StateType GetStateType()
    {
        return StateType.Idle;
    }

    public override void EnterState()
    {
        if (_playerStateMachine == null)
            return;
        Debug.Log("Enter - Idle");

        _playerStateMachine.HorizontalSpeed = 0f;
        _playerStateMachine.VerticalSpeed = 0f;
        _playerStateMachine.IsSprinting = false;
        _playerStateMachine.IsJumping = false;
        _playerStateMachine.IsFalling = false;
        _playerStateMachine.IsLanding = false;
    }

    public override StateType UpdateState()
    {
        if (_playerController == null)
            return StateType.None;

        if(Mathf.Abs(_playerController.HorizontalMove) > 0.2f || Mathf.Abs(_playerController.VerticalMove) > 0.2f)
        {
            return StateType.Walking;
        }
        
        if((Mathf.Abs(_playerController.HorizontalMove) > 0.2f || Mathf.Abs(_playerController.VerticalMove) > 0.2f)
            && _playerController.IsSprinting)
        {
            return StateType.Sprinting;
        }

        if((Mathf.Abs(_playerController.HorizontalMove) < 0.2f && Mathf.Abs(_playerController.VerticalMove) < 0.2f)
            && _playerController.IsEquippedWeapon)
        {
            return StateType.IdleWithRifle;
        }

        if (_playerController.IsJumping)
        {
            return StateType.JumpingUp;
        }


        return StateType.Idle;
    }

    public override void ExitState()
    {
        Debug.Log("Leave - Idle");
    }
}

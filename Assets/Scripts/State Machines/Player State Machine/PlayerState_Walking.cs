using UnityEngine;

public class PlayerState_Walking : PlayerBaseState
{
    public override StateType GetStateType()
    {
        return StateType.Walking;
    }

    public override void EnterState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Enter - Walking");
        _playerStateMachine.IsWalking = true;
        _playerStateMachine.IsSprinting = false;
        _playerStateMachine.IsJumping = false;
        _playerStateMachine.IsFalling = false;
        _playerStateMachine.IsLanding = false;
    }

    public override StateType UpdateState()
    {
        if (_playerController == null || _playerStateMachine == null)
            return StateType.None;

        _playerStateMachine.HorizontalSpeed = Mathf.Lerp(_playerStateMachine.HorizontalSpeed,
                                                         _playerController.HorizontalMove,
                                                         _playerStateMachine.SmoothSpeed * Time.deltaTime);
        
        _playerStateMachine.VerticalSpeed = Mathf.Lerp(_playerStateMachine.VerticalSpeed,
                                                       _playerController.VerticalMove,
                                                       _playerStateMachine.SmoothSpeed * Time.deltaTime);

        if(Mathf.Abs(_playerController.HorizontalMove) < 0.1f && Mathf.Abs(_playerController.VerticalMove) < 0.1f)
        {
            return StateType.Idle;
        }
        
        if((Mathf.Abs(_playerController.HorizontalMove) > 0.2f || Mathf.Abs(_playerController.VerticalMove) > 0.2f)
            && _playerController.IsSprinting)
        {
            return StateType.Sprinting;
        }
        
        if((Mathf.Abs(_playerController.HorizontalMove) > 0.2f || Mathf.Abs(_playerController.VerticalMove) > 0.2f)
            && _playerController.IsJumping)
        {
            return StateType.JumpingWithWalk;
        }


        return StateType.Walking;
    }

    public override void ExitState()
    {
        Debug.Log("Exit - Walking");

        if (_playerStateMachine == null)
            return;

        _playerStateMachine.IsWalking = false;
    }
}

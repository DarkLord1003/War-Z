using UnityEngine;

public class PlayerState_Sprinting : PlayerBaseState
{
    public override StateType GetStateType()
    {
        return StateType.Sprinting;
    }

    public override void EnterState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Enter - Sprinting");
        _playerStateMachine.IsSprinting = true;
        _playerStateMachine.IsWalking = false;
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

        if((Mathf.Abs(_playerController.HorizontalMove) > 0.2f || Mathf.Abs(_playerController.VerticalMove) > 0.2f)
            && !_playerController.IsSprinting)
        {
            return StateType.Walking;
        }


        return StateType.Sprinting;
    }

    public override void ExitState()
    {
        Debug.Log("Exit - Sprinting");

        if (_playerStateMachine == null)
            return;

        _playerStateMachine.IsSprinting = false;
    }
}

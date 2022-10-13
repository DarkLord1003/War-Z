using UnityEngine;

public class PlayerState_SprintWithRifle : PlayerBaseState
{
    public override StateType GetStateType()
    {
        return StateType.SprintWithRifle;
    }

    public override void EnterState()
    {
        if (_playerStateMachine == null)
            return;

        _playerStateMachine.IsWalking = false;
        _playerStateMachine.IsSprinting = true;
        _playerStateMachine.IsJumping = false;
        _playerStateMachine.IsFalling = false;
        _playerStateMachine.IsLanding = false;
    }

    public override StateType UpdateState()
    {
        if (_playerStateMachine == null || _playerController == null)
            return StateType.None;

        _playerStateMachine.HorizontalSpeed = Mathf.Lerp(_playerStateMachine.HorizontalSpeed,
                                                         _playerController.HorizontalMove,
                                                         _playerStateMachine.SmoothSpeed * Time.deltaTime);

        _playerStateMachine.VerticalSpeed = Mathf.Lerp(_playerStateMachine.VerticalSpeed,
                                                         _playerController.VerticalMove,
                                                         _playerStateMachine.SmoothSpeed * Time.deltaTime);

        if ((Mathf.Abs(_playerController.HorizontalMove) > 0.2f || Mathf.Abs(_playerController.VerticalMove) > 0.2f)
            && !_playerController.IsSprinting && _playerController.IsEquippedWeapon)
        {
            return StateType.WalkWithRifle;
        } 
        
        if ((Mathf.Abs(_playerController.HorizontalMove) > 0.2f || Mathf.Abs(_playerController.VerticalMove) > 0.2f)
            && !_playerController.IsSprinting && !_playerController.IsEquippedWeapon)
        {
            return StateType.Walking;
        }

        if (_playerController.IsJumping)
        {
            return StateType.JumpingWithSprint;
        }


        return StateType.SprintWithRifle;
    }

    public override void ExitState()
    {
        if (_playerStateMachine == null)
            return;

        Debug.Log("Exit - Sprint With Rifle");

        _playerStateMachine.IsSprinting = false;
    }
}

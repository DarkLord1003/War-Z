using UnityEngine;

public class PlayerState_Walking : PlayerBaseState
{
    private float _xVelocity;
    private float _yVelocity;

    public override StateType GetStateType()
    {
        return StateType.Walking;
    }


    public override void EnterState()
    {
        Debug.Log("STATE - WALKING");

        if (Animator == null)
            return;

        Animator.SetBool(IsWalkingHash, true);
        Animator.SetBool(IsSprintingHash, false);
        Animator.SetBool(IsJumpingHash, false);
        Animator.SetBool(IsFallingHash, false);
    }

    public override StateType UpdateState()
    {

        _xVelocity = Mathf.Lerp(_xVelocity, PlayerController.HorizontalMove, PlayerStateMachine.SmoothSpeed);
        _yVelocity = Mathf.Lerp(_yVelocity, PlayerController.VerticalMove, PlayerStateMachine.SmoothSpeed);

        Animator.SetFloat(XVelocityHash, _xVelocity);
        Animator.SetFloat(YVelocityHash, _yVelocity);

        if(Mathf.Abs(PlayerController.HorizontalMove) < 0.1f && Mathf.Abs(PlayerController.VerticalMove) < 0.1f)
        {
            return StateType.Idle;
        }
        
        
        if((Mathf.Abs(PlayerController.HorizontalMove) > 0.2f || Mathf.Abs(PlayerController.VerticalMove) > 0.2f)
            && PlayerController.IsSprinting)
        {
            return StateType.Sprinting;
        }

        return StateType.Walking;
    }

    public override void ExitState()
    {
        Debug.Log("EXIT - WALKING");

        Animator.SetBool(IsWalkingHash, false);
    }
}

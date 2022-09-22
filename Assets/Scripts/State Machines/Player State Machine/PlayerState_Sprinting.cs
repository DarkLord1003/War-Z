using UnityEngine;

public class PlayerState_Sprinting : PlayerBaseState
{
    private float _xVelocity;
    private float _yVelocity;

    public override StateType GetStateType()
    {
        return StateType.Sprinting;
    }

    public override void EnterState()
    {
        Debug.Log("ENTER - SPRINTING");

        if (Animator == null)
            return;

        Animator.SetBool(IsWalkingHash, false);
        Animator.SetBool(IsSprintingHash, true);
        Animator.SetBool(IsJumpingHash, false);
        Animator.SetBool(IsFallingHash, false);

    }

    public override StateType UpdateState()
    {
        _xVelocity = Mathf.Lerp(_xVelocity, PlayerController.HorizontalMove, PlayerStateMachine.SmoothSpeed);
        _yVelocity = Mathf.Lerp(_yVelocity, PlayerController.VerticalMove, PlayerStateMachine.SmoothSpeed);

        Animator.SetFloat(XVelocityHash, _xVelocity);
        Animator.SetFloat(YVelocityHash, _yVelocity);

        if ((Mathf.Abs(PlayerController.HorizontalMove) > 0.2f || Mathf.Abs(PlayerController.VerticalMove) > 0.2f)
            && !PlayerController.IsSprinting)
        {
            return StateType.Walking;
        }
        
        if((Mathf.Abs(PlayerController.HorizontalMove) < 0.1f && Mathf.Abs(PlayerController.VerticalMove) < 0.1f))
        {
            return StateType.Idle;
        }


        return StateType.Sprinting;
    }

    public override void ExitState()
    {
        Debug.Log("EXIT - SPRINTING");

        Animator.SetBool(IsSprintingHash, false);
    }


}

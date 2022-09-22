using UnityEngine;

public class PlayerState_Idle : PlayerBaseState
{

    public override StateType GetStateType()
    {
        return StateType.Idle;
    }
    public override void EnterState()
    {
        Debug.Log("STATE - IDLE");

        if (Animator == null)
            return;

        Animator.SetFloat(XVelocityHash, 0f, 0.2f, Time.deltaTime);
        Animator.SetFloat(YVelocityHash, 0f, 0.2f, Time.deltaTime);
        Animator.SetBool(IsWalkingHash, false);
        Animator.SetBool(IsSprintingHash, false);
        Animator.SetBool(IsJumpingHash, false);
        Animator.SetBool(IsFallingHash, false);
    }

    public override StateType UpdateState()
    {
        if((Mathf.Abs(PlayerController.HorizontalMove) > 0.2f || Mathf.Abs(PlayerController.VerticalMove) > 0.2f)
            && !PlayerController.IsSprinting)
        {
            return StateType.Walking;
        } 
        
        
        if((Mathf.Abs(PlayerController.HorizontalMove) > 0.2f || Mathf.Abs(PlayerController.VerticalMove) > 0.2f) 
            && PlayerController.IsSprinting)
        {
            return StateType.Sprinting;
        }

        if((Mathf.Abs(PlayerController.HorizontalMove) < 0.1f && Mathf.Abs(PlayerController.VerticalMove) < 0.1f)
            && PlayerController.IsJumping)
        {
            return StateType.JumpingUp;
        }

        return StateType.Idle;
    }

    public override void ExitState()
    {
        Debug.Log("EXIT - IDLE");
    }
}

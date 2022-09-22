using UnityEngine;

public class PlayerState_JumpingUp : PlayerBaseState
{
    public override StateType GetStateType()
    {
        return StateType.JumpingUp;
    }

    public override void EnterState()
    {
        Debug.Log("STATE - JUMPINGUP");

        if (PlayerStateMachine.Animator == null)
            return;

        Animator.SetBool(IsWalkingHash, false);
        Animator.SetBool(IsSprintingHash, false);
        Animator.SetBool(IsJumpingHash, true);
        Animator.SetBool(IsFallingHash, false);

    }

    public override StateType UpdateState()
    {
        if (PlayerController.IsFalling)
        {
            return StateType.Falling;
        }

        return StateType.JumpingUp;
    }

    public override void ExitState()
    {
        Debug.Log("EXIT - JUMPINGUP");

        Animator.SetBool(IsJumpingHash, false);
    }
}

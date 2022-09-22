using UnityEngine;

public abstract class PlayerBaseState 
{
    protected PlayerStateMachine PlayerStateMachine;
    protected PlayerController PlayerController;
    protected Animator Animator;

    protected int XVelocityHash = Animator.StringToHash("X_Velocity");
    protected int YVelocityHash = Animator.StringToHash("Y_Velocity");
    protected int IsWalkingHash = Animator.StringToHash("IsWalking");
    protected int IsSprintingHash = Animator.StringToHash("IsSprinting");
    protected int IsJumpingHash = Animator.StringToHash("IsJumping");
    protected int IsFallingHash = Animator.StringToHash("IsFalling");


    public virtual void SetStateMachine(PlayerStateMachine machine) => PlayerStateMachine = machine;
    public virtual void SetPlayerController(PlayerController controller) => PlayerController = controller;
    public virtual void SetAnimator(Animator anim) => Animator = anim;

    public abstract void EnterState();
    public abstract StateType UpdateState();
    public abstract void ExitState();

    public abstract StateType GetStateType();

}

using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected StateMachine _stateMachine;

    public virtual void SetStateMachine(StateMachine stateMachine) => _stateMachine = stateMachine;

    public virtual void EnterState() { }
    public virtual void ExitState() { }

    public abstract StateType GetStateType();
    public abstract StateType UpdateState();


}

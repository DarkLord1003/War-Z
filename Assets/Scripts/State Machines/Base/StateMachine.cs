using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    [Header("Current State")]
    [SerializeField] protected StateType CurrentStateType;

    protected Dictionary<StateType, State> _states;
    protected State CurrentState;

    protected Animator _animator;

    protected virtual void Update()
    {
        if (CurrentState == null)
            return;

        if (CurrentStateType == StateType.None)
            return;

        StateType newStateType = CurrentState.UpdateState();

        if(CurrentStateType != newStateType)
        {
            State newState = null;
            if(_states.TryGetValue(newStateType, out newState))
            {
                CurrentState.ExitState();
                newState.EnterState();
                CurrentState = newState;
            }
            else if(_states.TryGetValue(StateType.Idle, out newState))
            {
                CurrentState.ExitState();
                newState.EnterState();
                CurrentState = newState;
            }
            else
            {
                CurrentStateType = StateType.None;
            }

            CurrentStateType = newStateType;
        }
    }

    protected virtual void InitStates() { }
}

public enum StateType
{
    Idle,
    Walking,
    Sprinting,
    JumpingUp,
    Falling,
    Crouching,
    Landing,
    None,
    JumpingWithWalk,
    IdleWithRifle,
    WalkWithRifle,
    SprintWithRifle,
    JumpingWithSprint

}




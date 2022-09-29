using UnityEngine;

public abstract class PlayerBaseState:State
{
    protected PlayerController _playerController;
    protected PlayerStateMachine _playerStateMachine;

    public PlayerController PlayerController
    {
        set => _playerController = value;
    }

    public override void SetStateMachine(StateMachine stateMachine)
    {
        if(stateMachine.GetType() == typeof(PlayerStateMachine))
        {
            _playerStateMachine = stateMachine as PlayerStateMachine;
        }
    }
    public void SetPlayerController(PlayerController controller) => _playerController = controller;


}

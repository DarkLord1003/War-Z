using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerStateMachine))]
public class PlayerStateMachineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerStateMachine stateMachine = (PlayerStateMachine)target;

        if(stateMachine.IdleWithRifle != null)
        {
        }
    }
}

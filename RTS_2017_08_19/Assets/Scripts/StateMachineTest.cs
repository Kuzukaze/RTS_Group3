using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineTest : MonoBehaviour
{
    public enum STATE { Idle, Moving, Attack };
    public STATE currentState;
    public TextMesh myStateText;

    public List<StateMachineTest> visibleUnits;

    private void Start()
    {
        myStateText = GetComponentInChildren<TextMesh>();
        ChangeState(STATE.Idle);
    }

    public void ChangeState(STATE state)
    {
        currentState = state;
        if (myStateText != null) myStateText.text = state.ToString();
    }

}

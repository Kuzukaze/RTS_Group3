using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancel : BaseAction 
{
    public override void OnActionStarted()
    {
        GetComponent<TaskManager>().ClearPipe();
        BaseAction[] actions = GetComponents<BaseAction>();
        foreach (BaseAction action in actions)
        {
            action.CompleteAction();
        }
        //CompleteAction();
    }
        
}

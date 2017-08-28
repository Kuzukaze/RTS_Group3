using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTask : MonoBehaviour
{
    private BaseAction taskAction;
    private ActionType taskType;
    private Vector3 position;
    private Unit tagertUnit;
    private bool isHeadPipe;

    public PipeTask(BaseAction taskAction)
    {
       this.taskAction = taskAction;
        taskType = taskAction.GetActionType();
    }

    public PipeTask(Vector3 position, BaseAction taskAction)
    {
        this.taskAction = taskAction;
        taskType = taskAction.GetActionType();
        this.position = position;
    }

    public PipeTask(Unit tagertUnit, BaseAction taskAction)
    {
        this.taskAction = taskAction;
        taskType = taskAction.GetActionType();
        this.tagertUnit = tagertUnit;
    }
}

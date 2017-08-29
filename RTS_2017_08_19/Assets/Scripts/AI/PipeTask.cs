using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTask
{
    public BaseAction taskAction;
    private ActionType taskType;
    public Vector3 position;
    private Unit tagertUnit;
    private bool isHeadTask = false;
    private TaskManager myTaskManager;

    public PipeTask(BaseAction taskAction)
    {
       this.taskAction = taskAction;
       taskType = taskAction.GetActionType();
    }

    public PipeTask(BaseAction taskAction, Vector3 position)
    {
        this.taskAction = taskAction;
        taskType = taskAction.GetActionType();
        this.position = position;
    }

    public PipeTask(BaseAction taskAction, Unit tagertUnit)
    {
        this.taskAction = taskAction;
        taskType = taskAction.GetActionType();
        this.tagertUnit = tagertUnit;
    }

    public void Subscribe()
    {
        taskAction.ActionCompleteEvent += new ActionCompletionHandler(CompletionDetected);
    }

    public void CompletionDetected()
    {
        if (isHeadTask)
        {
            myTaskManager.RemoveHeadTask();
        }

    }
    
    public void SetTaskManager(TaskManager manager)
    {
        myTaskManager = manager;
    }

    public void IndicateHead ()
    {
        isHeadTask = true;
        Debug.Log("A task now listens to the BaseAction event");
        Subscribe();
    }
}

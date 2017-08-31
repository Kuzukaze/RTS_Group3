using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTask
{
    public BaseAction taskAction;
    private ActionType taskType;
    public Vector3 position;
    public Unit tagertUnit;
    private bool isHeadTask = false;
    private TaskManager myTaskManager;
    private bool canCallRemove = true;

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
        Debug.Log("EVENT JUST SUB");
    }


    public void CompletionDetected()
    {
        if (isHeadTask)
        {
            if (canCallRemove)
            {
                canCallRemove = false;
                Debug.Log("CompletionDetected");
                myTaskManager.RemoveHeadTask();
            }
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

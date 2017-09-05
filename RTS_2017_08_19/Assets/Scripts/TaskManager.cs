using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private Queue<PipeTask> tasksInPipe;
    private PipeTask localTask;
    private BaseAction newBaseAction;
    private Unit targetUnit;
    private Vector3 position;
    private BaseAction[] myActions;

    private void Start()
    {
        tasksInPipe = new Queue<PipeTask>();
        myActions = gameObject.GetComponents<BaseAction>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(tasksInPipe.Count);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            tasksInPipe.Enqueue(localTask);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            localTask.CompletionDetected();
        }

    }

    public void AddTask(BaseAction taskAction, bool clearTasks = true) 
    {
        if (!Input.GetKey(KeyCode.LeftShift) && clearTasks)
        {
            tasksInPipe.Clear();
        }

        PipeTask newTask = new PipeTask(taskAction);
        newTask.SetTaskManager(this);
        tasksInPipe.Enqueue(newTask);
        if (tasksInPipe.Count == 1)
        {
            ExecuteTask("AddTask");
        }
    }

    public void AddTask(BaseAction taskAction, Vector3 position, bool clearTasks = true)
    {
        if (!Input.GetKey(KeyCode.LeftShift) && clearTasks)
        {
            tasksInPipe.Clear();
        }

        PipeTask newTask = new PipeTask(taskAction, position);
        newTask.SetTaskManager(this);
        tasksInPipe.Enqueue(newTask);
        if (tasksInPipe.Count == 1)
        {
            ExecuteTask("AddTask");
        }
    }

    public void AddTask(BaseAction taskAction, Unit tagert, bool clearTasks = true)
    {
        if (!Input.GetKey(KeyCode.LeftShift) && clearTasks)
        {
            tasksInPipe.Clear();
        }

        PipeTask newTask = new PipeTask(taskAction, tagert);
        newTask.SetTaskManager(this);
        tasksInPipe.Enqueue(newTask);
        if (tasksInPipe.Count == 1)
        {
            ExecuteTask("AddTask");
        }
    }

    

    public void RemoveHeadTask()
    {
        if (tasksInPipe.Count > 0)
        {
            tasksInPipe.Dequeue();
            if(tasksInPipe.Count > 0)
            {
                ExecuteTask("RemoveHeadTask");
            }
        }
    }

    public void ClearPipe()
    {
        tasksInPipe.Clear();
        foreach (BaseAction action in myActions)
        {
            action.CompleteAction();
        }
    }

    public void ExecuteTask(string whoThis)
    {
   //     Debug.Log(string.Format("ExecuteTask was called by {0}", whoThis));
        if (tasksInPipe.Count > 0)
        {
        //    Debug.Log("Exexuting an action");
            localTask = tasksInPipe.Peek();
            localTask.IndicateHead();
            localTask.taskAction.ExecuteAction();
            localTask.taskAction.ExecuteAction(localTask.position);
            localTask.taskAction.ExecuteAction(localTask.tagertUnit);
        }

    }
}
    
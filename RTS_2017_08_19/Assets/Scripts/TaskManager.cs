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

    public void AddTask(BaseAction taskAction) //добавление инстант таска с очисткой трубы
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            AddTaskNoClear(taskAction);
        }
        else
        {
            ClearPipe();
            PipeTask newTask = new PipeTask(taskAction);
            newTask.SetTaskManager(this);
            tasksInPipe.Enqueue(newTask);
            if (tasksInPipe.Count == 1)
            {
                ExecuteTask("AddTask");
            }
        }

    }

    public void AddTaskNoClear(BaseAction taskAction)
    {
        PipeTask newTask = new PipeTask(taskAction);
        newTask.SetTaskManager(this);
        tasksInPipe.Enqueue(newTask);
        if (tasksInPipe.Count == 1)
        {
            ExecuteTask("AddTask");
        }
    }

    public void AddTask(BaseAction taskAction, Vector3 position) //добавление вектор таска с очисткой трубы
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            AddTaskNoClear(taskAction, position);
        }
        else
        {
            ClearPipe();
            PipeTask newTask = new PipeTask(taskAction, position);
            newTask.SetTaskManager(this);
            tasksInPipe.Enqueue(newTask);
            if (tasksInPipe.Count == 1)
            {
                ExecuteTask("AddTask");
            }
        }
    }

    public void AddTaskNoClear(BaseAction taskAction, Vector3 position)
    {
            PipeTask newTask = new PipeTask(taskAction, position);
            newTask.SetTaskManager(this);
            tasksInPipe.Enqueue(newTask);
            if (tasksInPipe.Count == 1)
            {
                ExecuteTask("AddTask");
            }
    }

    public void AddTask(BaseAction taskAction, Unit tagert) //добавление юнит таска с очисткой трубы
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            AddTaskNoClear(taskAction, tagert);
        }
        else
        {
            ClearPipe();
            PipeTask newTask = new PipeTask(taskAction, tagert);
            newTask.SetTaskManager(this);
            tasksInPipe.Enqueue(newTask);
            if (tasksInPipe.Count == 1)
            {
                ExecuteTask("AddTask");
            }
        }
    }

    public void AddTaskNoClear(BaseAction taskAction, Unit tagert)
    {
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
    
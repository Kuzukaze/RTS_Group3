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

    private void Start()
    {
        tasksInPipe = new Queue<PipeTask>();
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
            tasksInPipe.Dequeue();
            ExecuteTask();
        }

    }

    public void AddTask(PipeTask newTask)
    {
        tasksInPipe.Enqueue(newTask);
        ExecuteTask();
    }

    public void ExecuteTask()
    {
        if (tasksInPipe.Count > 0)
        {
            localTask = tasksInPipe.Peek();
        }
        localTask.taskAction.ExecuteAction(localTask.position);
    }

}
    
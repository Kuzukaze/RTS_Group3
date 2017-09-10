using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionColliderPipe : MonoBehaviour
{
    [SerializeField] Attack action;

    private Unit myUnit;
    private TaskManager myTaskManager;

    private void Start()
    {
        myUnit = gameObject.GetComponent<Unit>();
        myTaskManager = gameObject.GetComponent<TaskManager>();
        if (myUnit == null)
        {
            myUnit = transform.parent.GetComponent<Unit>();
        }
        if (myTaskManager == null)
        {
            myTaskManager = transform.parent.GetComponent<TaskManager>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Unit collided = other.gameObject.GetComponent<Unit>();
        if (collided != null && !collided.Equals(myUnit) && !collided.Team.Equals(myUnit.Team))
        {
            myTaskManager.AddTask(action, collided, false);
        }
    }
}

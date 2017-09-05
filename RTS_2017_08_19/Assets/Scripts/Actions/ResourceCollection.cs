using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : BaseAction
{
    [SerializeField] private ResourceData.ResourceType resourceType;
    [SerializeField] private uint resourceAmount;
    [SerializeField] private float collectionTime;
    [SerializeField] private float currentTime;
    [SerializeField] private float triggerDistance;
    [SerializeField] private float stopDistance;

    [SerializeField] private BaseAction mover;
    [SerializeField] private PlayerController player;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private Unit targetGenerator;

    [SerializeField] private bool isTransporting = false;
    [SerializeField] private bool isStoppedCollecting = false;
    [SerializeField] private bool isСollecting = false;

    public override void Start()
    {
        base.Start();
        //Debug.Log("Resource collection started");

        player = GameManager.Instance.LevelManager.CurrentPlayer;
        taskManager = this.gameObject.GetComponent<TaskManager>();
        
        //mover = this.gameObject.GetComponent<MoveAction>();   
    }

    public override void OnActionStarted()
    {
    }

    public override void OnActionStarted(Unit target)
    {
        if (isTransporting)
        {
            Vector3 closestStorage = player.GetClosestResourceStoragePosition(this.transform.position, resourceType);
            if(Vector3.Distance(closestStorage, this.transform.position) > triggerDistance)
            {
                taskManager.AddTask(mover, closestStorage - Quaternion.LookRotation(closestStorage - this.transform.position) * Vector3.one * stopDistance, false);
                taskManager.AddTask(this, targetGenerator, false);
                CompleteAction();
                return;
            }

            Cargo cargo = this.gameObject.GetComponent<Cargo>();
            player.GetResourceByType(resourceType).Add(cargo.Amount);
            Destroy(cargo);

            if(target && target.GetComponent<ResourceGenerator>())
            {
                targetGenerator = target;
            }

            taskManager.AddTask(this, targetGenerator, false);
            //Debug.Break();
            isTransporting = false;
            CompleteAction();

            return;
        }

        if(target == null)
        {
            Debug.Break();
            return;
        }

        if (target.GetComponent<ResourceGenerator>() == null)
        {
            Debug.Log("Incorrect target.");
            CompleteAction();
            return; 
        }
        else
        {
            if(targetGenerator != target)
            {
                targetGenerator = target;
            }
        }


        
        if (Vector3.Distance(this.transform.position, target.transform.position) > triggerDistance)
        {
            //Debug.Log("Too far. Moving to target.");

            Vector3 posToMove = target.transform.position - Quaternion.LookRotation(target.transform.position - this.transform.position)*Vector3.one * stopDistance;

            taskManager.AddTask(mover, posToMove, false);

            //taskManager.AddTask(mover, new Vector3 (10,0,10) , false);
            taskManager.AddTask(this, target, false);
            

            CompleteAction();
            return; 
        }
        else
        {
            //Debug.Log("ResourceCollection started");
            currentTime = collectionTime;
            isСollecting = true;
        }

    }

    public override void OnActionInProgress(Unit target)
    {
        if(isСollecting)
        {
            //Debug.Log("ResourceCollection in progress");
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                //Debug.Log("ResourceCollection CompleteAction() call");
                isStoppedCollecting = true;
                isСollecting = false;
                if(target != null)
                {
                    targetGenerator = target;
                }
                CompleteAction();
            }
        }
        else
        {
            CompleteAction();
        }
    }

    public override void OnActionComplete()
    {
        if(isStoppedCollecting)
        {
            //Debug.Log("ResourceCollection completed");
            Cargo cargo = this.gameObject.AddComponent<Cargo>();
            cargo.Init(ResourceData.cargoID.Matter, (int)resourceAmount);

            Vector3 storagePosition = player.GetClosestResourceStoragePosition(this.transform.position, resourceType);

            taskManager.AddTask(mover, storagePosition - Quaternion.LookRotation(storagePosition - this.transform.position) * Vector3.one * stopDistance, false);
            isTransporting = true;
            isStoppedCollecting = false;

            taskManager.AddTask(this, false);
        }
    }
}

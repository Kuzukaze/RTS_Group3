/* How to use BaseAction:
 * 1) Override OnActionStarted (either with no parameters, or Vector3, or Unit). This function is called when ExecuteAction occurs.
 * 2) *optional* Override OnActionInProgress(). This method is called each Update until the action ends.
 * 3) To end the action, call the "CompleteAction()" method. IF YOUR ACTION IS INSTANT, CALL CompleteAction() AT THE END OF YOUR VERSION OF OnActionStarted() 
 * 4) *optional* Override OnActionComplete(). This method is called after CompleteAction() is called
 * 
 * ActionCompleteEvent is called when CompleteAction() is called. You can subscribe to this event like this:
 * 
    private BaseAction someBaseAction;
    
    public void Subscribe()
    {
        someBaseAction.ActionCompleteEvent += new ActionCompletionHandler(CompletionDetected);
    }

    public void CompletionDetected()
    {
        Debug.Log("Check event {0}");
    }
    */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ActionCompletionHandler();

public enum ActionType {instant, terrainClick, unitClick, construction};

public class BaseAction : MonoBehaviour {

    public event ActionCompletionHandler ActionCompleteEvent;

    [SerializeField] protected Sprite icon;
    [SerializeField] protected ActionType actionType;
    [SerializeField] protected int id;
    [SerializeField] protected bool locked = false;
    [SerializeField] protected int actionToUnlock;

    private UnlockManager unlockManager;

    protected Vector3 targetPosition;
    protected Unit targetUnit;

    private bool actionInProgress = false;

    [SerializeField] private bool defaultMoveAction = false;
    [SerializeField] private bool defaultAttackAction = false;

    public ActionType GetActionType ()
    {
        return actionType;
    }

    public Sprite GetIcon ()
    {
        return icon;
    }

    public int GetID()
    {
        return id;
    }

    protected void CompleteAction()
    { //calll this method when the action is complete
        //Debug.Log("actionInProgress = false");
        actionInProgress = false;
        OnActionComplete();

        if (ActionCompleteEvent != null)
        {
            ActionCompleteEvent();
        }

        //if (targetPosition != null)
        //{
        //    targetPosition = Vector3.zero;
        //}
        if (targetUnit != null)
        {
            targetUnit = null;
        }
    }

    public void ExecuteAction () 
    {
        actionInProgress = true;
        //Debug.Log("ExecuteActionnn ()");
        OnActionStarted();
    }

    public void ExecuteAction (Vector3 pos)  
    {
        actionInProgress = true;
        //Debug.Log("ExecuteAction (Vector3 pos)");
        targetPosition = pos;
        Debug.Log(string.Format("targetPosition was set to {0}",targetPosition));
        OnActionStarted(pos);
    }

    public void ExecuteAction (Unit target)  
    {
        //Debug.Log(string.Format("ExecuteAction (Unit target) was started from {0}", whoThis));
        actionInProgress = true;
        targetUnit = target;
        OnActionStarted(target);
    }

    public virtual void DrawPreActionMarker(Vector3 pos)
    {
        actionInProgress = true;
        //Debug.Log("DrawPreActionMarker (Vector3 pos)");
    }

    public bool IsLocked ()
    {
        return locked;
    }

    public virtual void OnActionStarted()
    {

    }

    public virtual void OnActionStarted(Vector3 pos)
    {

    }

    public virtual void OnActionStarted(Unit target)
    {

    }

    public virtual void OnActionInProgress()
    {

    }

    public virtual void OnActionInProgress(Vector3 pos)
    {

    }

    public virtual void OnActionInProgress(Unit target)
    {

    }

    public virtual void OnActionComplete()
    {

    }
 

    public virtual void Start()
    {
        unlockManager = FindObjectOfType<UnlockManager>();
        locked = unlockManager.CheckIfLocked(id);
        unlockManager.ActionUnlocked += new ActionUnlockHandler(UnlockDetected);
    }

    void Update ()
    {
        if (actionInProgress)
        {
            //Debug.Log("In progress true");
            switch (actionType)
            {
                case ActionType.instant:
                    OnActionInProgress();
                    break;
                case ActionType.terrainClick:
                    OnActionInProgress(targetPosition);
                    break;
                case ActionType.construction:
                    OnActionInProgress(targetPosition);
                    break;
                case ActionType.unitClick:
                    OnActionInProgress(targetUnit);
                    break;
            }
        }
    } 
     

    public void UnlockDetected (int unlockedID)
    {
        if (unlockedID == id)
            locked = false;
    }

    public void UnlockAction (int actionID)
    {
        Debug.Log(string.Format("BaseAction: trying to unlock action {0}", actionID));
        unlockManager.UnlockAction(actionID);
    }

    public bool IsDefaultMoveAction()
    {
        return defaultMoveAction;
    }

    public bool IsDefaultAttackAction()
    {
        return defaultAttackAction;
    }

    //---------EVENT SUBSCRIPTION EXAMPLE:--------------------
    /*
    private BaseAction someBaseAction;
    
    public void Subscribe()
    {
        someBaseAction.ActionCompleteEvent += new ActionCompletionHandler(CompletionDetected);
    }

    public void CompletionDetected()
    {
        Debug.Log("Check event {0}");
    }
    */
}

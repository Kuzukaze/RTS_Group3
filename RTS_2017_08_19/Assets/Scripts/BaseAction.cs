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

    protected bool isShowingGhost = false;
    private UnlockManager unlockManager;

    private bool actionInProgress = false;

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
        if (ActionCompleteEvent != null)
            ActionCompleteEvent();
        actionInProgress = false;
        OnActionComplete();
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
        OnActionStarted(pos);
    }

    public void ExecuteAction (Unit target)  
    {
        //Debug.Log("ExecuteAction (Unit target)");
        actionInProgress = true;
        OnActionStarted(target);
    }

    public virtual void DrawPreActionMarker(Vector3 pos)
    {
        actionInProgress = true;
        //Debug.Log("DrawPreActionMarker (Vector3 pos)");
    }

    public void SetShowingGhost()
    {
        isShowingGhost = !isShowingGhost;
    }

    public void SetShowingGhost(bool val)
    {
        isShowingGhost = val;
    }

    public bool IsShowingGhost()
    {
        return isShowingGhost;
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
            OnActionInProgress();
        }
    }
     

    public void UnlockDetected (int unlockedID)
    {
        if (unlockedID == id)
            locked = false;
    }

    public void UnlockAction (int actionID)
    {
        unlockManager.UnlockAction(actionID);
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

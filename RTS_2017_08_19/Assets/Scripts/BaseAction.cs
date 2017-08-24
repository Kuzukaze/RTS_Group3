using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ActionCompletionHandler();

public enum ActionType {instant, terrainClick, unitClick, construction};

public class BaseAction : MonoBehaviour {

    public event ActionCompletionHandler ActionComplete;

    [SerializeField] protected Sprite icon;
    [SerializeField] protected ActionType actionType;
    [SerializeField] protected int id;
    [SerializeField] protected bool locked = false;
    [SerializeField] protected int actionToUnlock;

    protected bool isShowingGhost = false;
    private UnlockManager unlockManager;

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

    protected void SignalCompletion()
    { //calll this method when the action is complete
        this.ActionComplete();
    }

    public virtual void ExecuteAction () 
    {
        Debug.Log("ExecuteActionnn ()");
    }

    public virtual void ExecuteAction (Vector3 pos)  
    {
        Debug.Log("ExecuteAction (Vector3 pos)");
    }

    public virtual void ExecuteAction (Unit target)  
    {
        Debug.Log("ExecuteAction (Unit target)");
    }

    public virtual void DrawPreActionMarker(Vector3 pos)
    {
        Debug.Log("DrawPreActionMarker (Vector3 pos)");
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

 

    public virtual void Start()
    {
        unlockManager = FindObjectOfType<UnlockManager>();
        locked = unlockManager.CheckIfLocked(id);
        unlockManager.ActionUnlocked += new ActionUnlockHandler(UnlockDetected);
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
        someBaseAction.ActionComplete += new ActionCompletionHandler(CompletionDetected);
    }

    public void CompletionDetected()
    {
        Debug.Log("Check event {0}");
    }
    */
}

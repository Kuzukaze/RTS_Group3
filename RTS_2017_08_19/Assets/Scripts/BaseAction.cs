using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType {instant, terrainClick, unitClick, construction};

public class BaseAction : MonoBehaviour {

    [SerializeField] protected Sprite icon;
    [SerializeField] protected ActionType actionType;
    [SerializeField] protected int id;
    protected bool busy = false;

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

    public void SetBusy()
    {
        busy = !busy;
    }
        
    public void SetBusy(bool val)
    {
        busy = val;
    }

    public bool IsBusy()
    {
        return busy;
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

}

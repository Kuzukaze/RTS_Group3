using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ActionUnlockHandler(int eventID);

public class UnlockManager : MonoBehaviour {
    [SerializeField]
    private List<int> unlockedActions;
    public event ActionUnlockHandler ActionUnlocked;

    void Awake () {
        //unlockedActions = new List<int>();
        BaseAction[] initialActions = FindObjectsOfType<BaseAction>();
        foreach (BaseAction current in initialActions)
        {
            if (!unlockedActions.Contains(current.GetID()) && !current.IsLocked())
            {
                unlockedActions.Add(current.GetID());
                Debug.Log(string.Format("Unlock Manager: Added {0} to unlocked actions", current.GetID()));
            }
        }
	}
        

    public void UnlockAction(int actionID)
    {
        if (!unlockedActions.Contains(actionID))
        {
            unlockedActions.Add(actionID);
            this.ActionUnlocked(actionID);
        }
        else
        {
            Debug.Log(string.Format("Unlock Manager: Trying to unlock actionID {0}, which is already unlocked", actionID));
        }
    }

    public bool CheckIfLocked(int actionID)
    {
        if (unlockedActions != null)
        {
            return !unlockedActions.Contains(actionID);
        }
        else
        {
            Debug.Log("Unlock Manager: Error! Unlocked actions list is null, can't check if an action is unlocked!");
            return false;
        }
    }
	
}

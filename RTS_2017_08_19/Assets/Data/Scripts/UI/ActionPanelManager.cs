using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionPanelManager : MonoBehaviour {

    private ActionPanel[] actionPanels;

    private UIManager uiManager;

	void Start () {
        actionPanels = GetComponentsInChildren<ActionPanel>();
        uiManager = GetComponentInParent<UIManager>();
        uiManager.SetCurrentActions(null);
	}
	
    public void AddAction (BaseAction action)
    { 
        int i = 0;
        while (i < actionPanels.Length)
        { //searching for a good slot to add the action
            if (i >= actionPanels.Length)
                return;
            ActionPanel currentPanel = actionPanels[i];
            if (currentPanel.IsOccupied()) 
            {//check if the current action has the same id
                if ((currentPanel.GetActionID() == action.GetID()) && (currentPanel.GetActionID() != -1)) //-1 is an error code
                {
                    currentPanel.AddActionToList(action); 
                    return;
                }
            }
            if (currentPanel.IsFree()) //if there's no action with the same id, search for any free slot
            {
                currentPanel.SetAction (action); 
                return;
            }
            i++;
        }
    }

    public void SetNewSelection(ActionPanel requester)
    { 
        foreach ( ActionPanel current in actionPanels)
        { //unselect all other panels except requester
            if (current != requester)
            {
                current.Unselect();
            }
        }
        uiManager.SetCurrentActions(requester.GetCurrentActions());
    }

    public void UnselectAll()
    { //unselect all panels
        foreach (ActionPanel current in actionPanels)
        {
            current.Unselect();
            uiManager.SetCurrentActions(null);
        }
    }


    public void ClearActions()
    { //clear all panels from actions
        foreach (ActionPanel current in actionPanels)
        {
            current.Unselect();
            //current.RemoveAction();
            current.ClearAction();
            uiManager.SetCurrentActions(null); 
        }
    }

    public List<BaseAction> GetDefaultMoveActions()
    {
        Debug.Log("GetDefaultMoveActions started");
        for (int i=0; i< actionPanels.Length; i++)
        {
            if (actionPanels[i].GetCurrentActions() != null)
            {
                Debug.Log(string.Format("Action panel {0} is default move action: {1}", i, actionPanels[i].GetCurrentActions()[0].IsDefaultMoveAction()));
                if (actionPanels[i].GetCurrentActions()[0].IsDefaultMoveAction())
                    return actionPanels[i].GetCurrentActions();
            }
        }
        return null;
    }

    public List<BaseAction> GetDefaultAttackActions()
    {
        Debug.Log("GetDefaultAttackActions started");
        for (int i=0; i< actionPanels.Length; i++)
        {
            if (actionPanels[i].GetCurrentActions() != null)
            {
                Debug.Log(string.Format("Action panel {0} is default attack action: {1}", i, actionPanels[i].GetCurrentActions()[0].IsDefaultAttackAction()));
                if (actionPanels[i].GetCurrentActions()[0].IsDefaultAttackAction())
                    return actionPanels[i].GetCurrentActions();
            }
        }
        return null;
    }

}

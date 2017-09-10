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
        FindObjectOfType<EventHub>().ActionPanelUpdateRequest += new ActionPanelUpdateRequestHandler(UpdateActionPanels);
	}
	
    public void AddAction (BaseAction action)
    { 
        if (action.GetID() == BaseActions.Cancel)
        { //reserved for the "cancel" action with id 404. Must be placed into the last slot.
            ActionPanel targetPanel = actionPanels[actionPanels.Length-1];
            if (targetPanel.IsOccupied())
            {
                targetPanel.AddActionToList(action);
            }
            else
            {
                targetPanel.SetAction(action);
            }
            return;
        }

        //for all other actions:
        int i = 0;
        while (i < actionPanels.Length)
        { //searching for a good slot to add the action
            if (i >= actionPanels.Length)
                return;
            ActionPanel currentPanel = actionPanels[i];
            if (currentPanel.IsOccupied()) 
            {//check if the current action has the same id
                if ((currentPanel.GetActionID() == action.GetID()) && (currentPanel.GetActionID() != BaseActions.Error)) //-1 is an error code
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
        UpdateActionPanels();
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
        if (requester.GetCurrentActions()[0].GetActionType() == ActionType.instant)
        {
            requester.Unselect();
            uiManager.ExecuteCurrentAction();
        }
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
            current.ClearAction();
            uiManager.SetCurrentActions(null); 
        }
    }

    public List<BaseAction> GetDefaultMoveActions()
    {
        for (int i=0; i< actionPanels.Length; i++)
        {
            if (actionPanels[i].GetCurrentActions() != null)
            {
                List<BaseAction> actionList = new List<BaseAction>();
                //Debug.Log(string.Format("Action panel {0} is default move action: {1}", i, actionPanels[i].GetCurrentActions()[0].IsDefaultMoveAction()));
                if (actionPanels[i].GetCurrentActions()[0].IsDefaultMoveAction())
                {
                    //return actionPanels[i].GetCurrentActions();
                    foreach (BaseAction current in actionPanels[i].GetCurrentActions())
                    {
                        actionList.Add(current);
                    }
                }
                return actionList;
            }
        }
        return null;
    }

    public List<BaseAction> GetDefaultAttackActions()
    {
        for (int i=0; i< actionPanels.Length; i++)
        {
            if (actionPanels[i].GetCurrentActions() != null)
            {
                //Debug.Log(string.Format("Action panel {0} is default attack action: {1}", i, actionPanels[i].GetCurrentActions()[0].IsDefaultAttackAction()));
                if (actionPanels[i].GetCurrentActions()[0].IsDefaultAttackAction())
                    return actionPanels[i].GetCurrentActions();
            }
        }
        return null;
    }

    public void FindAndRemoveActions (BaseAction[] actionsToRemove)
    {
        foreach (BaseAction currentActionToRemove in actionsToRemove)
        {
            foreach (ActionPanel currentPanel in actionPanels)
            {
                if (currentPanel.GetActionID() == currentActionToRemove.GetID())
                {
                    currentPanel.RemoveActionFromList(currentActionToRemove);
                }
            }
        }
    }

    public void UpdateActionPanels()
    {
        foreach (ActionPanel currentPanel in actionPanels)
        {
            currentPanel.ScheduleColorCheck();
        }
    }

    public void StopAllActions()
    {
        foreach (ActionPanel currentPanel in actionPanels)
        {
            currentPanel.CompleteActions();
        }
    }

}

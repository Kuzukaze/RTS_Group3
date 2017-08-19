using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ActionDelegate (Vector3 position);

public class UIManager : MonoBehaviour {

    List<BaseAction> currentActions;

    ActionPanelManager actionPanelManager;

    [SerializeField] private Image infoPic;

    [SerializeField] private Sprite emptyInfoPicSprite;

    [SerializeField] private Text infoText;


    //---------------------------------------------------
    //---------------------------------------------------
    //-------CALL THESE FROM GAME/PLAYER MANAGER---------

    public void AddActions (BaseAction[] actions) 
    {
        foreach (BaseAction action in actions)
        {
            actionPanelManager.AddAction(action);
        }
    }

    public void SetInfoPic (Sprite pic)
    {
        infoPic.sprite = pic;
    }

    public void SetInfoText (string text)
    {
        infoText.text = text;
    }

    public void UnselectObject ()
    {
        actionPanelManager.ClearActions();
        infoPic.sprite = emptyInfoPicSprite;
        infoText.text = "";
    }

    public void ExecuteCurrentAction ()
    {
        if (currentActions != null) //if we have an action selected
        { 
            if (currentActions[0].GetActionType() == ActionType.instant) //if the action type is appropriate
            {
                foreach (BaseAction action in currentActions) //execute the action for each unit
                {
                    action.ExecuteAction(); 
                }
            }
        }
    }

    public void ExecuteCurrentAction (Vector3 position)
    {
        if (currentActions != null)
        { 
            if (currentActions[0].GetActionType() == ActionType.terrainClick)
            {
                foreach (BaseAction action in currentActions)
                {
                    action.ExecuteAction(position);
                }
            }
        }
    }

    public void ExecuteCurrentAction (Unit target)
    {
        if (currentActions != null)
        { 
            if (currentActions[0].GetActionType() == ActionType.unitClick)
            {
                foreach (BaseAction action in currentActions)
                {
                    action.ExecuteAction(target);
                }
            }
        }
    }

    //---------------------------------------------------
    //---------------------------------------------------
    //---------------------------------------------------
    //USE AT YOUR OWN RISK (not intended to be called from outside):

    void Start ()
    {
        actionPanelManager = GetComponentInChildren<ActionPanelManager>();
    }

    public void AddAction (BaseAction action)
    {
        actionPanelManager.AddAction (action);
    }

    public void UnselectAction ()
    {
        actionPanelManager.UnselectAll();
    }

    public void SetCurrentActionAsNull()
    {
        SetCurrentActions(null);
    }

    public void SetCurrentActions (List<BaseAction> actions)
    {
        currentActions = actions;
    }
       

}

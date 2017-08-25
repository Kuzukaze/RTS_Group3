using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public delegate void ActionDelegate (Vector3 position);

public enum UIStates {NoObjectSelected, NoActionSelected, PointActionSelected, GhostShown};

public class UIManager : MonoBehaviour {

    const UIStates NO_OBJECT_SELECTED = UIStates.NoObjectSelected;
    const UIStates NO_ACTION_SELECTED = UIStates.NoActionSelected;
    const UIStates POINT_ACTION_SELECTED = UIStates.PointActionSelected;
    const UIStates GHOST_SHOWN = UIStates.GhostShown;

    private UIStates currentState = NO_OBJECT_SELECTED;
    private UIStates lastState = NO_OBJECT_SELECTED;

    List<BaseAction> currentActions;
    List<BaseAction> currentMoveActions;
    private bool defaultActionsUpToDate = false;
    List<BaseAction> currentAttackActions;

    ActionPanelManager actionPanelManager;

    [SerializeField] private Image infoPic;

    [SerializeField] private Sprite emptyInfoPicSprite;

    [SerializeField] private Text infoText;
    //---------------------------------------------------
    //---------------------------------------------------
    //-------CALL THESE FROM GAME/PLAYER MANAGER---------

    public void AddActions (BaseAction[] actions) 
    {
        //Debug.Log(string.Format("AddAction is called from {0}", author));
        foreach (BaseAction action in actions)
        {
            actionPanelManager.AddAction(action);
        }
        //Debug.Log("AddActions is switching current state to NO_ACTIONS_SELECTED");
        currentState = NO_ACTION_SELECTED;
        defaultActionsUpToDate = false;
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
        currentState = NO_OBJECT_SELECTED;
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

    void Update ()
    { 
        if (lastState != currentState)
            Debug.Log(string.Format("Switched from state {0} to state {1}", lastState, currentState));

        switch (currentState)
        {
            case NO_OBJECT_SELECTED:
                break;
            case POINT_ACTION_SELECTED:
                PointActionState();
                break;
            case NO_ACTION_SELECTED:
                NoActionState();
                break;
            case GHOST_SHOWN:
                GhostShownState();
                break;
        }
        lastState = currentState;
    }

    void NoObjectState()
    {
        defaultActionsUpToDate = false;
    }

    void NoActionState()
    {
        if (!defaultActionsUpToDate)
        {
            currentMoveActions = actionPanelManager.GetDefaultMoveActions();
            currentAttackActions = actionPanelManager.GetDefaultAttackActions();
            defaultActionsUpToDate = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Got mouse butt");
            Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit interactionInfo;
            if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            {
                if (interactionInfo.collider.gameObject.GetComponent<ClickableGround>() != null && currentMoveActions !=null)
                {
                    Debug.Log("Got clickable ground");
                    foreach (BaseAction action in currentMoveActions)
                    {
                        action.ExecuteAction(interactionInfo.point);
                    }
                }
                else if (interactionInfo.collider.gameObject.GetComponent<Unit>() != null && currentAttackActions !=null)
                {
                    Debug.Log("Got unit");
                    foreach (BaseAction action in currentAttackActions)
                    {
                        action.ExecuteAction(interactionInfo.collider.gameObject.GetComponent<Unit>());
                    }
                }
            }
        }
    }

    void PointActionState()
    {
        if (currentActions != null && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit interactionInfo;
                if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
                {
                    ProcessPointRaycastHit(interactionInfo);
                }
            }
        }
    }

    void ProcessPointRaycastHit(RaycastHit hit)
    {
        if (hit.collider.gameObject.GetComponent<Unit>() != null)
        {
            ExecuteCurrentAction(hit.collider.gameObject.GetComponent<Unit>());
            return;
        }

        if (hit.collider.gameObject.GetComponent<ClickableGround>() != null)
        {
            ExecuteCurrentAction(hit.point);
            return;
        }
    }

    void GhostShownState()
    {
        if (currentActions != null && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit interactionInfo;
            if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            {
                currentActions[0].DrawPreActionMarker(interactionInfo.point);
            }

            if (Input.GetMouseButtonDown(1))
            {
                currentActions[0].ExecuteAction(interactionInfo.point);
                UnselectAction();
            }
        }
    }


    void Start ()
    {
        actionPanelManager = GetComponentInChildren<ActionPanelManager>();
    }
        
    public void UnselectAction ()
    {
        actionPanelManager.UnselectAll();
        Debug.Log("UnselectAction is switching current state to NO_ACTIONS_SELECTED");
        currentState = NO_ACTION_SELECTED;
    }
        

    public void SetCurrentActions (List<BaseAction> actions)
    {
        if (actions == null)
        {
            currentState = NO_ACTION_SELECTED;
            return;
        }
        currentActions = actions;
        ActionType currentType = currentActions[0].GetActionType();
        if (currentType == ActionType.terrainClick || currentType == ActionType.unitClick)
            currentState = POINT_ACTION_SELECTED;
        else if (currentType == ActionType.construction)
            currentState = GHOST_SHOWN;
    }
       

}

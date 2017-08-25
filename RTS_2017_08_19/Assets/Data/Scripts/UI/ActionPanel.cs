using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionPanel : MonoBehaviour, IPointerDownHandler {

    private Image backgroundImage;
    private Image displayedImage;
    private ActionPanelManager actionManager;

    private List<BaseAction> currentActions;

    [SerializeField]
    private Sprite emptyIcon;

    [SerializeField]
    private Color selectedColor;

    [SerializeField]
    private Color unselectedColor;

    [SerializeField]
    private Color lockedColor;

    private bool occupied = false;
    private bool selected = false;
    private bool checkColorInLateUpdate;

	// Use this for initialization
	void Start () {
        backgroundImage = GetComponent<Image>();
        displayedImage = GetComponentsInChildren<Image>()[1];
        displayedImage.sprite = emptyIcon;
        actionManager = GetComponentInParent<ActionPanelManager>();

        UnlockManager unlockManager = FindObjectOfType<UnlockManager>();
        unlockManager.ActionUnlocked += new ActionUnlockHandler(UnlockDetected);
	}

    public void UnlockDetected (int unlockedID)
    {
        checkColorInLateUpdate = true;
    }

    public bool IsFree()
    {
        return !occupied;
    }

    public bool IsOccupied()
    {
        return occupied;
    }

    public void SetAction(BaseAction action)
    {
        occupied = true;
        displayedImage.sprite = action.GetIcon();
        if (currentActions != null)
            currentActions.Clear();
        currentActions = new List<BaseAction>();
        currentActions.Add(action);
        if (currentActions[0].IsLocked())
            backgroundImage.color = lockedColor;
        checkColorInLateUpdate = true;
    }

    public void AddActionToList (BaseAction action)
    {
        currentActions.Add(action);
    }

    public List<BaseAction> GetCurrentActions ()
    {
        return currentActions;
    }

    public int GetActionID()
    {
        if (currentActions != null)
            return currentActions[0].GetID();
        else
            return -1;
    }

    public void ClearAction()
    {
        occupied = false;
        displayedImage.sprite = emptyIcon;
        if (currentActions != null)
            //currentActions.Clear();
            currentActions = null;
        checkColorInLateUpdate = true;
    }

    public void Select()
    {
        selected = true;
        //Debug.Log(currentActions[0].IsShowingGhost());
        //currentActions[0].SetShowingGhost(false);
        backgroundImage.color = selectedColor;
        actionManager.SetNewSelection(this);
    }

    public void Unselect()
    {
        selected = false;
        CheckIfLocked();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentActions != null)
        {
            if (!currentActions[0].IsLocked())
            {
                if (selected)
                {
                    actionManager.UnselectAll();
                }
                else
                {
                    if (occupied) //if the slot is not empty
                    SelectOrExecute();
                }
            }
        }
    }

    public void SelectOrExecute ()
    {
        if (currentActions[0].GetActionType() == ActionType.instant)
        { //if an instant action, execute immediatly
            foreach (BaseAction action in currentActions)
            {
                action.ExecuteAction();
            }
            actionManager.UnselectAll();
        }
        else
        { //if not an instant action, prepare to select a target
            Select();
        }
    }
        
    public void CheckIfLocked ()
    {
        if (currentActions != null)
        {
            if (currentActions.Count > 0)
            {
               // Debug.Log(string.Format("My action is {0}, it's locked: {1}", currentActions[0].GetID(), currentActions[0].IsLocked()));
                if (currentActions[0].IsLocked())
                    backgroundImage.color = lockedColor;
                else
                    backgroundImage.color = unselectedColor;
            }
        }
        else
        { 
            //Debug.Log("No action detected");
            backgroundImage.color = unselectedColor;
        }
    }

    void LateUpdate()
    {
        if (checkColorInLateUpdate)
        {
            CheckIfLocked();
            if (selected)
                backgroundImage.color = selectedColor;
            checkColorInLateUpdate = false;
        }
    }

    public void ScheduleColorCheck()
    {
        checkColorInLateUpdate = true;
    }
}

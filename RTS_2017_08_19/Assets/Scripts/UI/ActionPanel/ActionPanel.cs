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

    public void UnlockDetected (BaseActions unlockedID)
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

    public BaseActions GetActionID()
    {
        if (currentActions != null && currentActions.Count != 0)
            return currentActions[0].GetID();
        else
            return (BaseActions)(-1);
    }

    public void ClearAction()
    {
        occupied = false;
        displayedImage.sprite = emptyIcon;
        if (currentActions != null)
            currentActions = null;
        checkColorInLateUpdate = true;
    }

    public void Select()
    {
        selected = true;
        checkColorInLateUpdate = true;
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
                    Select();
                }
            }
        }
    }

      
    public void CheckIfLocked ()
    {
        if (currentActions != null)
        {
            if (currentActions.Count > 0)
            {
                if (currentActions[0].IsLocked())
                    backgroundImage.color = lockedColor;
                else if (!selected)
                    backgroundImage.color = unselectedColor;
                else
                    backgroundImage.color = selectedColor;
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
            checkColorInLateUpdate = false;
        }
    }

    public void ScheduleColorCheck()
    {
        checkColorInLateUpdate = true;
    }

    public void RemoveActionFromList (BaseAction actionToRemove)
    {
        for (int i = currentActions.Count - 1; i>=0; i--)
        {
            if (currentActions[i] == actionToRemove)
            {
                currentActions.RemoveAt(i);
            }
        }
    }

    public void CompleteActions()
    {
        foreach (BaseAction current in currentActions)
        {
            Debug.Log(string.Format("Completing action {0}",current));
            current.CompleteAction();
        }
    }
}

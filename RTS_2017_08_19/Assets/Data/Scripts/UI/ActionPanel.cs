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

    private bool occupied = false;
    private bool selected = false;

	// Use this for initialization
	void Start () {
        backgroundImage = GetComponent<Image>();
        displayedImage = GetComponentsInChildren<Image>()[1];
        displayedImage.sprite = emptyIcon;

        actionManager = GetComponentInParent<ActionPanelManager>();
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
            currentActions.Clear();
    }

    public void Select()
    {
        selected = true;
        Debug.Log(currentActions[0].IsBusy());
        currentActions[0].SetBusy(false);
        backgroundImage.color = selectedColor;
        actionManager.SetNewSelection(this);
    }

    public void Unselect()
    {
        selected = false;
        backgroundImage.color = unselectedColor;
    }

    public void OnPointerDown(PointerEventData eventData)
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
        
}

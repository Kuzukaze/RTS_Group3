using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    [SerializeField] private List<Selectable> selectableObjectsList;
    [SerializeField] private List<Selectable> selectedObjectsList;  
    [SerializeField] private UIManager uiManager;
    
    private SelectionType currentSelectionType;
    private bool isSelecting = false;
    private Vector3 mousePosition;

    private Selectable firstSelected = null;

    void Start()
    {
        selectableObjectsList = GameManager.Instance.SelectableObjects;
        selectedObjectsList = new List<Selectable>();
    }

    public void OnMouseButtonDown(Vector3 mousePosition)
    {
        isSelecting = true;
        this.mousePosition = mousePosition;

        //check if we instantly click on Selectable
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rhInfo;
        firstSelected = null;
        if (Physics.Raycast(ray, out rhInfo))
        {
            Selectable selectedObject = rhInfo.transform.GetComponent<Selectable>();
            if (selectedObject)         //if click on Selectable - clear lists & add first
            {
                foreach (var selectableObject in selectableObjectsList)
                {
                    selectableObject.Deselect();
                }
                uiManager.UnselectObject();
                selectedObjectsList.Clear();

                firstSelected = selectedObject;
                selectedObjectsList.Add(firstSelected);

                //UISelect(selectedObject);
                selectedObject.Select();
            }
        }

    }
    public void OnMouseButtonUp()
    {
        bool isSelectionEmpty = true;
        SetLowestSelectionType(ref currentSelectionType);

        //fill selectoedObjectList & find higher priority selection type
        foreach (var selectableObject in selectableObjectsList)
        {
            if (IsWithinSelectionBounds(selectableObject.gameObject))
            {
                if(isSelectionEmpty)
                {
                    if(firstSelected == null)
                    {
                        selectedObjectsList.Clear();
                    }
                    isSelectionEmpty = false;
                    uiManager.UnselectObject();
                }
                selectedObjectsList.Add(selectableObject);
                if(currentSelectionType < selectableObject.Type)
                {
                    currentSelectionType = selectableObject.Type;
                }
            }
            else
            {
                if (firstSelected != selectableObject)
                {
                    selectableObject.Deselect();
                }
                else
                {
                    if (isSelectionEmpty)
                    {
                        isSelectionEmpty = false;
                        if (currentSelectionType < selectableObject.Type)
                        {
                            currentSelectionType = selectableObject.Type;
                        }
                    }
                }
            }
        }

        //if empty - show old selection. Else select new objects with currentSelectionType
        if (isSelectionEmpty)
        {
            foreach(var selectedObject in selectedObjectsList)
            {
                selectedObject.Select();
            }
        }
        else
        {
            foreach (var selectedObject in selectedObjectsList)
            {
                if (selectedObject.Type != currentSelectionType)
                {
                    selectedObject.Deselect();
                }
                else
                {
                    selectedObject.Select();
                    UISelect(selectedObject);
                }
            }
            selectedObjectsList.RemoveAll(selected => selected.Type != currentSelectionType);
        }

        isSelecting = false;
    }

    private void UISelect(Selectable selectableObject)
    {
        uiManager.SetInfoPic(selectableObject.Icon);
        uiManager.SetInfoText(selectableObject.Description);
        BaseAction[] actions = selectableObject.GetComponents<BaseAction>();
        uiManager.AddActions(actions);
    }

    void Update()
    {
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            OnMouseButtonDown(Input.mousePosition);
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp();
        }

        //Highlight all objects within the selection box
        if (isSelecting)
        {
            foreach (var selectableObject in selectableObjectsList)
            {
                if (IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    selectableObject.Select();
                }
                else
                {
                    if(firstSelected != selectableObject)
                    {
                        selectableObject.Deselect();
                    }
                }
            }
        }
    }


    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
        {
            return false;
        }

        var camera = Camera.main;
        var viewportBounds = Utils.GetViewportBounds(camera, mousePosition, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(mousePosition, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }


    public enum SelectionType
    {
        building = 1,
        engineer = 2,
        unit = 3
    }
    public static void SetLowestSelectionType(ref SelectionType selectionType)
    {
        selectionType = (SelectionType)1;
    }
}

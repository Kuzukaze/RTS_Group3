using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] private bool isSelected = false;
    [SerializeField] private GameObject selectionPrefab;       //TODO GO, which visualize this object is selected
    private GameObject selectionGlow = null;

    [SerializeField] private Sprite selectedIconSprite;
    [SerializeField] private string selectedDescription;
    [SerializeField] private SelectionManager.SelectionType selectionType;


    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;
        }
    }
    public Sprite Icon
    {
        get
        {
            return selectedIconSprite;
        }
    }
    public string Description
    {
        get
        {
            return selectedDescription;
        }
    }
    public SelectionManager.SelectionType Type
    {
        get
        {
            return selectionType;
        }
    }


    // Use this for initialization
    void Start()
    {
        GameManager.Instance.AddSelectableObject(this); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        if (!isSelected)
        {
            isSelected = true;

            selectionGlow = (GameObject)GameObject.Instantiate(selectionPrefab);
            selectionGlow.transform.parent = transform;
            selectionGlow.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    public void Deselect()
    {
        if(isSelected)
        {
            isSelected = false;

            GameObject.Destroy(selectionGlow);
            selectionGlow = null;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveSelectableObject(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Selectable> selectableObjectsList;

    //TODO must be here? Or in Selection Manager...
    public List<Selectable> SelectableObjects
    {
        get
        {
            return selectableObjectsList;
        }
    }
    public void AddSelectableObject(Selectable selected)
    {
        selectableObjectsList.Add(selected);
    }
    public void RemoveSelectableObject(Selectable selected)
    {
        if(selected)
        {
            selectableObjectsList.Remove(selected);
        }
    }
    public void RemoveSelectableObjectByID(int id)
    {
        if(selectableObjectsList != null && selectableObjectsList[id] != null)
        {
            selectableObjectsList.RemoveAt(id);
        }
    }
    public void ClearSelectableObjectsList()
    {
        selectableObjectsList.Clear();
    }

    private bool isInit = false;
    private static GameManager instance;

    private GameManager()
    {

    }
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }
    }

 private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.transform.gameObject);

        selectableObjectsList = new List<Selectable>();
    }
    void Start()
    {
        Init();
    }
    void Init()
    {

        isInit = true;
    }
    public bool IsInit()
    {
        return isInit;
    }
   
    void Update()
    {

    }
}

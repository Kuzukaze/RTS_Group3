using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    [SerializeField]private Selectable selectableUnit;
    [SerializeField]private EngeneerBuilder engeneerUnit;

    void OnDestroy ()
    {
        Debug.Log("Building got destroyed");
    }

    /* Ушло в компонент EngeneerBuilder
    public void Skill()
    { 
     if (Input.GetKeyDown(KeyCode.B) && selectableUnit.IsSelected)
       {
          engeneerUnit.PlaceEnengineer();
       }
    }
    */
}



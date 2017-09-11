using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    [SerializeField]private Selectable selectableUnit;
    [SerializeField]private UnitBuilder buildedUnit;

    void OnDestroy ()
    {
        Debug.Log("Building got destroyed");
    }
}



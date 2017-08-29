using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableGround : MonoBehaviour {

    [SerializeField] private bool allowBuilding = true;

    public bool BuildingAllowed()
    {
        return allowBuilding;
    }
        

}

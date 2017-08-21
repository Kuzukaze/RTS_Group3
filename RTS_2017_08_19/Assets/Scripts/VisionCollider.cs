using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCollider : Vision
{
    private List<Unit> visibleUnitsList;
    private Unit myUnit;

    private void Start()
    {
        visibleUnitsList = new List<Unit>();
        myUnit = gameObject.GetComponent<Unit>();
        if (myUnit == null)
        {
            myUnit = transform.parent.GetComponent<Unit>();
        }
    }

    public override List<Unit> VisibleUnits()
    {
        return visibleUnitsList;
    }

    public void OnTriggerEnter(Collider other)
    {
        Unit collided = other.gameObject.GetComponent<Unit>();
        if (collided != null &&  !collided.Equals(myUnit))
        {
            visibleUnitsList.Add(collided);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Unit collided = other.gameObject.GetComponent<Unit>();
        if (collided != null)
        {
            for (int i=0; i < visibleUnitsList.Count; i++)
            {
                visibleUnitsList.RemoveAt(i);
                break;
            }
        }
    }
}

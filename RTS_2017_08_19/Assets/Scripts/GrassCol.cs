using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCol : MonoBehaviour
{
    private Unit myUnits;
    private List<Unit> myUnitsInGrass;


    private void Start()
    {
        myUnitsInGrass = new List<Unit>();

        myUnits = gameObject.GetComponent<Unit>();
        if (myUnits == null)
        {
            myUnits = transform.parent.GetComponent<Unit>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Unit collided = other.gameObject.GetComponent<Unit>();
        if (collided != null && !collided.Equals(myUnits))
        {
            myUnitsInGrass.Add(collided);
            collided.slowDown(5);
            Debug.Log("Зашел");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        Unit collided = other.gameObject.GetComponent<Unit>();
        if (collided != null)
        {
            for (int i = 0; i < myUnitsInGrass.Count; i++)
            {
                if (myUnitsInGrass[i].Equals(collided))
                {
                    myUnitsInGrass.RemoveAt(i);
                    collided.slowDown(-5);
                    Debug.Log("Вышел");
                    break;
                }
            }
        }
    }

}

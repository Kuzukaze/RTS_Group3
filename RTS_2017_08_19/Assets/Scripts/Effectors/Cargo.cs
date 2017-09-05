using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Effector

//Describes cargo id & amount carryed by object 
public class Cargo : MonoBehaviour
{
    [SerializeField] private ResourceData.cargoID cargoID;
    [SerializeField] private int cargoAmount;
    
    public ResourceData.cargoID ID
    {
        get
        {
            return cargoID;
        }
    }
    public int Amount
    {
        get
        {
            return cargoAmount;
        }
    }

    public void Init(ResourceData.cargoID id, int amount)
    {
        cargoID = id;
        cargoAmount = amount;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private bool isFinite = false;
    [SerializeField] private uint maxResource;

    [SerializeField] private uint resourceIncomeRate;


    // Use this for initialization
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnDestroy()
    {
        
    }
}

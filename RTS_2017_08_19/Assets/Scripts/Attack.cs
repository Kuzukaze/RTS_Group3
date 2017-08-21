using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : BaseAction
{
    [SerializeField]Transform firePoint;
    [SerializeField]float range;


    public override void ExecuteAction(Unit tagertUnit)
    {
        if (range > (firePoint.transform.position - tagertUnit.transform.position).sqrMagnitude)
        {
         RaycastHit hit;
            Physics.Raycast(firePoint.transform.position, tagertUnit.transform.position, out hit);
            Debug.DrawLine(firePoint.transform.position, tagertUnit.transform.position, Color.green, 2);
            tagertUnit.TakeDamage(50);
            Debug.Log("hit");
        }
        else
        {
            GetComponent<NavMeshAgent>().destination = tagertUnit.transform.position;
        }
    }
}

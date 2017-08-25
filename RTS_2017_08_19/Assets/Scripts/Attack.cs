﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : BaseAction
{
    [SerializeField]Transform firePoint;
    [SerializeField]float range;
    [SerializeField]float reloadTime;
    [SerializeField]float damageDone;

    private float shortCounter = 0;

    private void Update()
    {
        shortCounter -= Time.deltaTime;
    }


    public override void OnActionStarted(Unit tagertUnit)
    {
        if (shortCounter <= 0)
        {
            shortCounter = reloadTime;

            if (range > (firePoint.transform.position - tagertUnit.transform.position).sqrMagnitude)
            {
                RaycastHit hit;
                Physics.Raycast(firePoint.transform.position, tagertUnit.transform.position, out hit);
                Debug.DrawLine(firePoint.transform.position, tagertUnit.transform.position, Color.green, 2);
                tagertUnit.TakeDamage(damageDone);
            }
            else
            {
                GetComponent<NavMeshAgent>().destination = tagertUnit.transform.position;  //стейтмашин.
            }

        }
        CompleteAction();
    }
}


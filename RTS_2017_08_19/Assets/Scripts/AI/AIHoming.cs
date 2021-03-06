﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIHoming : MonoBehaviour
{
    [SerializeField]private Vision vision;

    [SerializeField] private ResourceData.Teams myTeam;
    private Unit myUnit;
    private NavMeshAgent myUnitNavMesh;

    private void Start()
    {
        myUnit = GetComponent<Unit>();
        myUnitNavMesh = GetComponent<NavMeshAgent>();
        myTeam = myUnit.Team;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        List<Unit> unitList = vision.VisibleUnits();
        float minDistance = Mathf.Infinity;
        Unit unit = null;

        for (int i = 0; i < unitList.Count; i++)
        {
            float distance = (transform.position - unitList[i].transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                unit = unitList[i];
            }
        }
        if (unit != null && myTeam != unit.Team)
        {
            myUnitNavMesh.destination = unit.transform.position;
            if ((transform.position - unit.transform.position).sqrMagnitude <= 5)  // Передеделать в стейт машину, придумать как интегрировать в Selectable Юниты.
            {
                unit.TakeDamage(5);
            }
        }
    }


}

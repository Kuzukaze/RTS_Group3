using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoverGround : BaseAction
{
    private NavMeshAgent unitNavMesh;

    private void Start()
    {
        unitNavMesh = GetComponent<NavMeshAgent>();
    }

    public override void ExecuteAction(Vector3 pos)
    {
            unitNavMesh.destination = pos;
        }
    }


/* Ушло в ClikableGround
 
public void MoveRMC(NavMeshAgent unitNavMesh)
{
    Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit interactionInfo;
    if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
    {
        unitNavMesh.destination = interactionInfo.point;

    }
}
*/



/*
class InputMgr
{
    void Update()
    {
        if(mouse && neUnit)
        {
            GameMgr.mouseClicked(Input.mousePosition);
        }
    }
}

public class GameMgr
{
    public static Action<Vector3> mouseClicked;
    public static Action<Unit> UnitClicked;
}

public class Action2
{
    public virtual void Act() { }
    public virtual OnUnitSelected() { }
    public virtual OnUnitUnselected() { }
}

public class move : Action2
{
    public override void Act()
    {
        base.Act();
        GameMgr.UnitClicked += Go;
    }

    private void Go(Unit pos) { Go(pos.position); }
    private void Go(Vector3 pos) { }

    public override void OnUnitSelected()
    {

        GameMgr.mouseClicked += Go;
    }
    public override void OnUnitSelected()
    {

        GameMgr.mouseClicked -= Go;
    }
}

public class attack : Action2
{
    public override void Act()
    {
        base.Act();
        GameMgr.mouseClicked += Attack;
    }

    private void Attack(Vector3 pos) { }
    private void Attack(Unit pos) { }

    public override void OnUnitSelected()
    {

        GameMgr.UnitClicked += Attack;
    }
    public override void OnUnitSelected()
    {

        GameMgr.mouseClicked -= Attack;
    }
}
*/

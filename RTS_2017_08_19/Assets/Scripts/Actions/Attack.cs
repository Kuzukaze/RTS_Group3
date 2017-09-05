using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : BaseAction
{
    [SerializeField] Transform firePoint;
    [SerializeField] protected float range;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected float damageDone;
    [SerializeField] private LayerMask layerMask;

    protected float shortCounter = 0;

    private NavMeshAgent unitNavMesh;


    public override void Start()
    {
        base.Start();
        unitNavMesh = GetComponent<NavMeshAgent>();
        layerMask = ~(1 << LayerMask.NameToLayer("MiniMap"));

        FindObjectOfType<EventHub>().UnitDeathEvent += new UnitDeathHandler(UnitDeathDetected);
    }


    public override void OnActionInProgress(Unit targetUnit)
    {
        shortCounter -= Time.deltaTime;

        if (targetUnit == null)
        {
            CompleteAction();
        }
        else
        {
            if (shortCounter <= 0)
            {
                shortCounter = reloadTime;
                if (range >= (firePoint.transform.position - targetUnit.transform.position).sqrMagnitude)
                {
                    unitNavMesh.velocity = Vector3.zero;
                    unitNavMesh.isStopped = true;
                    unitNavMesh.ResetPath();
                    RaycastHit hit;
                    Physics.Raycast(firePoint.transform.position, targetUnit.transform.position, out hit, layerMask);
                    Debug.DrawLine(firePoint.transform.position, targetUnit.transform.position, Color.green, 2);
                    targetUnit.TakeDamage(damageDone);
                }
                else if (range < (firePoint.transform.position - targetUnit.transform.position).sqrMagnitude)
                {
                    unitNavMesh.destination = targetUnit.transform.position;
                }
            }
        }


    }

    public void UnitDeathDetected(Unit killedUnit)
    {
        if (killedUnit = targetUnit)
        {
            CompleteAction();
            Debug.Log("Unit Die");
        }
    }
}


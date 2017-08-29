using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : BaseAction
{
    [SerializeField]Transform firePoint;
    [SerializeField]float range;
    [SerializeField]float reloadTime;
    [SerializeField]float damageDone;
    [SerializeField] private LayerMask layerMask;

    private float shortCounter = 0;

    

    public override void Start()
    {
        base.Start();
        layerMask = ~(1 << LayerMask.NameToLayer("MiniMap"));
    }


    public override void OnActionInProgress(Unit target)
    {
        //Debug.Log("in prog");
        shortCounter -= Time.deltaTime;
        if (shortCounter <= 0)
        {
            shortCounter = reloadTime;
            if (range > (firePoint.transform.position - targetUnit.transform.position).sqrMagnitude)
            {
                RaycastHit hit;
                Physics.Raycast(firePoint.transform.position, targetUnit.transform.position, out hit, layerMask);
                Debug.DrawLine(firePoint.transform.position, targetUnit.transform.position, Color.green, 2);
                targetUnit.TakeDamage(damageDone);

            }
            else
            {
                CompleteAction();
                //Debug.Log("in comp");
            }
        }
    }
}


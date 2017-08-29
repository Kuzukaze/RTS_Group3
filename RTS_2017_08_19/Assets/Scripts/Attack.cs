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

<<<<<<< HEAD
    public override void OnActionInProgress(Unit target)
=======
    private void Start()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("MiniMap"));
    }

    private void Update()
>>>>>>> master
    {
        //Debug.Log("in prog");
        shortCounter -= Time.deltaTime;
        if (shortCounter <= 0)
        {
            shortCounter = reloadTime;
            if (range > (firePoint.transform.position - targetUnit.transform.position).sqrMagnitude)
            {
                RaycastHit hit;
<<<<<<< HEAD
                Physics.Raycast(firePoint.transform.position, targetUnit.transform.position, out hit);
                Debug.DrawLine(firePoint.transform.position, targetUnit.transform.position, Color.green, 2);
            targetUnit.TakeDamage(damageDone);
=======
                Physics.Raycast(firePoint.transform.position, tagertUnit.transform.position, out hit, layerMask);
                Debug.DrawLine(firePoint.transform.position, tagertUnit.transform.position, Color.green, 2);
                tagertUnit.TakeDamage(damageDone);
>>>>>>> master
            }
            else
            {
                CompleteAction();
                //Debug.Log("in comp");
            }
        }
    }
}


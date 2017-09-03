using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngeneerBuilder : BaseAction
{
    [SerializeField]GameObject engineerPrefab;
    private GameObject engineer = null;

    private Selectable selectable;

    private float radius = 0.2f;

    public override void Start()
    {
        base.Start();
        selectable = GetComponent<Selectable>();
    }

    public override void OnActionStarted(Vector3 pos)
    {
        if (isNoBuildingsNearby(pos))
        {
            //engineer = (GameObject)GameObject.Instantiate(engineerPrefab, pos, engineerPrefab.transform.rotation);
            engineer = (GameObject)GameObject.Instantiate(engineerPrefab, transform.position + new Vector3(0,0,-3), Quaternion.LookRotation(Vector3.back));
            PlayerController player = this.gameObject.GetComponent<Unit>().Player;
            engineer.GetComponent<Unit>().Init(player);
            UnlockAction(actionToUnlock);
            MoverGround mover = engineer.GetComponent<MoverGround>();
            if (mover != null)
            {
                mover.ExecuteAction(pos);
            }
            else
            {
                MoverAir airMover = engineer.GetComponent<MoverAir>();
                if (airMover != null)
                {
                    airMover.ExecuteAction(pos);
                }
            }
        }
        CompleteAction();
    }

    bool isNoBuildingsNearby(Vector3 hitPoint)
    {
        Collider[] hitColliders;
        hitColliders = Physics.OverlapSphere(hitPoint, radius);
        bool isEmpty = true;

        foreach (Collider obj in hitColliders)
        {
            if (obj.tag != "Ground")
            {
                isEmpty = false;
                Debug.Log("Здание!");
            }
        }

        return isEmpty;
    }

}

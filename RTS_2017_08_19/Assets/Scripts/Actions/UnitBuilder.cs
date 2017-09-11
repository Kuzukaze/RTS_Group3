using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBuilder : BaseAction
{
    [SerializeField]GameObject unitPrefab;
    private GameObject unit = null;

    private Selectable selectable;

    private float radius = 0.2f;

    [SerializeField] private float buildingTime = 2.0f;
    private float currentTime = 0.0f;

    [SerializeField] private Image progressBar;

    public override void Start()
    {
        base.Start();
        selectable = GetComponent<Selectable>();
        progressBar.fillAmount = 0;
    }

    public override void OnActionStarted(Vector3 pos)
    {
        if (isNoBuildingsNearby(pos))
        {
            locked = true;
            eventHub.RequestActionPanelUpdate();
        }
        else
        {
            CompleteAction();
        }

    }

    public override void OnActionInProgress(Vector3 pos)
    {
        currentTime += Time.deltaTime;
        progressBar.fillAmount = currentTime / buildingTime;
        if (currentTime >= buildingTime)
        {
            currentTime = 0.0f;
            progressBar.fillAmount = 0;
            unit = (GameObject)GameObject.Instantiate(unitPrefab, transform.position + new Vector3(0,0,-3), Quaternion.LookRotation(Vector3.back));
            PlayerController player = this.gameObject.GetComponent<Unit>().Player;
            unit.GetComponent<Unit>().Init(player);
            UnlockAction(actionToUnlock);
            MoverGround mover = unit.GetComponent<MoverGround>();
            if (mover != null)
            {
                mover.ExecuteAction(pos);
            }
            else
            {
                MoverAir airMover = unit.GetComponent<MoverAir>();
                if (airMover != null)
                {
                    airMover.ExecuteAction(pos);
                }
            }
            locked = false;
            eventHub.RequestActionPanelUpdate();
            CompleteAction();
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartBuilder : BaseAction
{
    [SerializeField] GameObject buildingPrefab;
    [SerializeField] GameObject ghostbuildingOK;
    [SerializeField] GameObject ghostbuildingFail;

    private GameObject currentGhost;
    private Selectable selectable;
    private float radius = 2f;

    public override void Start()
    {
        base.Start();
        selectable = GetComponent<Selectable>();
    }

    public override void OnActionStarted(Vector3 pos)
    {
        Debug.Log("Execute action");
        if (isNoBuildingsNearby(pos))
        {
            Instantiate(buildingPrefab, pos, buildingPrefab.transform.rotation);
            UnlockAction(actionToUnlock);
        }
        Destroy(currentGhost);
        //this.SetShowingGhost(false);
        CompleteAction();
    }

    public override void DrawPreActionMarker(Vector3 position)
    {
        if(currentGhost == null)
        {
            if (isNoBuildingsNearby(position))
            {
                currentGhost = Instantiate(ghostbuildingOK, position, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                currentGhost = Instantiate(ghostbuildingFail, position, Quaternion.Euler(0, 0, 0));
            }
            currentGhost.transform.position = position;
            currentGhost.GetComponent<GhostKill>().MarkAsNeeded();
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

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
    private bool wasOccupied = false;

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
            GameObject instantiated = Instantiate(buildingPrefab, pos, buildingPrefab.transform.rotation);
            PlayerController player = this.gameObject.GetComponent<Unit>().Player;
            instantiated.GetComponent<Unit>().Init(player);
            UnlockAction(actionToUnlock);
        }
        Destroy(currentGhost);
        //this.SetShowingGhost(false);
        CompleteAction();
    }

    public override void DrawPreActionMarker(Vector3 position)
    {
        if(currentGhost == null || wasOccupied != isNoBuildingsNearby(position))
        {
            SpawnGhost(position);
            wasOccupied = isNoBuildingsNearby(position);
        }
        currentGhost.transform.position = position;
        currentGhost.GetComponent<GhostKill>().MarkAsNeeded();
    }

    void SpawnGhost(Vector3 position)
    {
        if (isNoBuildingsNearby(position))
        {
            currentGhost = Instantiate(ghostbuildingOK, position, Quaternion.Euler(0, 0, 0));
        }
        else
        {
            currentGhost = Instantiate(ghostbuildingFail, position, Quaternion.Euler(0, 0, 0));
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

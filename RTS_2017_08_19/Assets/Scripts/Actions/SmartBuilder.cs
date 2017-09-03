using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SmartBuilder : BaseAction
{
    [SerializeField] GameObject buildingPrefab;
    [SerializeField] GameObject buildingConstruction;
    [SerializeField] GameObject ghostbuildingOK;
    [SerializeField] GameObject ghostbuildingFail;

    private GameObject currentGhost;
    private Selectable selectable;
    private float radius = 2f;
    private bool wasOccupied = false;
    private float timer = 0;
    private float range = 50;
    private NavMeshAgent unitNavMesh;
    private bool isBuildingConstracting = false;
    private Vector3 buildingPlace;

    public override void Start()
    {
        base.Start();
        selectable = GetComponent<Selectable>();
        unitNavMesh = GetComponent<NavMeshAgent>();
    }

    public override void OnActionStarted(Vector3 pos)
    {

        Destroy(currentGhost);
        //this.SetShowingGhost(false);

    }

    public override void OnActionInProgress(Vector3 pos)
    {
        if (isNoBuildingsNearby(pos) && (range) >= (unitNavMesh.transform.position - pos).sqrMagnitude && !isBuildingConstracting)
        {
            unitNavMesh.velocity = Vector3.zero;
            unitNavMesh.isStopped = true;
            unitNavMesh.ResetPath();
            GameObject instantiated = Instantiate(buildingConstruction, pos, buildingPrefab.transform.rotation);
            PlayerController player = this.gameObject.GetComponent<Unit>().Player;
            instantiated.GetComponent<Unit>().Init(player);
            isBuildingConstracting = true;
        }
        else if (isNoBuildingsNearby(pos) && range < (unitNavMesh.transform.position - pos).sqrMagnitude && !isBuildingConstracting)
        {
            unitNavMesh.destination = pos;
        }

        if (isBuildingConstracting)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                isBuildingConstracting = false;
                buildingPlace = pos;
                CompleteAction();
                Debug.Log("CompBuild");
                timer = 0;
            }
        }



    }

    public override void OnActionComplete()
    {
        Destroy(buildingConstruction);
        GameObject instantiated = Instantiate(buildingPrefab, buildingPlace, buildingPrefab.transform.rotation);
        PlayerController player = this.gameObject.GetComponent<Unit>().Player;
        instantiated.GetComponent<Unit>().Init(player);
        UnlockAction(actionToUnlock);
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

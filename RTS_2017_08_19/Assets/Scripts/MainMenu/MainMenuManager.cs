using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private float minTimeBetweenEvents = 2;
    [SerializeField] private float maxTimeBetweenEvents = 5;
    private float lastEvent;
    private float nextEvent;

    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] jetSpawnPoints;
    [SerializeField] private Transform[] targets;

    [SerializeField] private GameObject[] groundUnits;
    [SerializeField] private GameObject jetUnit;
    [SerializeField] private float unitLifetime = 10;
    [SerializeField] private Unit turret;
    private GameObject currentUnit;
    private bool hasToInitUnit = false;

    void Start()
    {
        turret.gameObject.AddComponent<UIDisabler>();
        turret.GetComponent<AttackTurret>().ExecuteAction(targets[Random.Range(0,targets.Length)].GetComponent<Unit>());
        Debug.Log("adding component");
        RandomEvent();
        lastEvent = Time.time;
        nextEvent = lastEvent + Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
    }

    void Update ()
    {
        if (Time.time > nextEvent)
        {
            RandomEvent();
            lastEvent = Time.time;
            nextEvent = lastEvent + Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
        }
    }

    void RandomEvent()
    {
        int n = Random.Range(0, 3);
        switch (n)
        {
            case 0:
                currentUnit = SpawnUnit();
                currentUnit.AddComponent<UIDisabler>();
                break;
            case 1:
                currentUnit = SpawnUnit();
                currentUnit.AddComponent<UIDisabler>();
                break;
            case 2:
                currentUnit = SpawnPlane();
                currentUnit.AddComponent<UIDisabler>();
                break;
        }

    }

    public GameObject SpawnUnit ()
    {
        hasToInitUnit = true;
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        return Instantiate(groundUnits[Random.Range(0, groundUnits.Length)], spawnPoint.position, spawnPoint.rotation);
    }

    public GameObject SpawnPlane ()
    {
        hasToInitUnit = true;
        Transform jetSpawnPoint = jetSpawnPoints[Random.Range(0, jetSpawnPoints.Length)];
        return Instantiate(jetUnit, jetSpawnPoint.position, jetSpawnPoint.rotation);
    }

    public void LateUpdate()
    {
        if (hasToInitUnit && Time.time == lastEvent)
        {
            currentUnit.AddComponent<DestroyTimer>();
            Unit target = targets[Random.Range(0, targets.Length)].GetComponent<Unit>();
            if (currentUnit.GetComponent<MoverGround>() != null)
            {
                Transform wayPoint = wayPoints[Random.Range(0, wayPoints.Length)];
                currentUnit.GetComponent<MoverGround>().ExecuteAction(wayPoint.position);
            }
            else
            {
                Transform wayPoint = targets[Random.Range(0, targets.Length)];
                currentUnit.GetComponent<MoverAir>().ExecuteAction(wayPoint.position);
            }
            currentUnit.GetComponent<AttackTurret>().ExecuteAction(target);
            currentUnit.GetComponent<Unit>().SetLineColor(new Color(0,0,0,0));
            hasToInitUnit = false;
        }
    }
        
}

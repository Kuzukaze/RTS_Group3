using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Vector3> startPositions;
    [SerializeField] private List<PlayerController> players;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(List<Vector3> startPositions,  List<PlayerController> players)
    {
        this.startPositions = startPositions;
        this.players = players;

        foreach(PlayerController player in players)
        {
            Unit mainBuilding = Instantiate<Unit>(ResourceData.Instance.mainBuildingRace1Prefab);
            mainBuilding.Init(player);
            mainBuilding.transform.position = startPositions[player.StartPosition];
        }
    }
}

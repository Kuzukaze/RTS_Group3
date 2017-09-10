using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Vector3> startPositions;
    [SerializeField] private List<PlayerController> players;
    [SerializeField] private GameObject playersGO;
    [SerializeField] private PlayerController currentPlayer;
    
    public GameObject PlayersGO
    {
        get
        {
            return playersGO;
        }
    }
    public PlayerController GetPlayerController(int position)
    {
        return players[position];
    }
    public PlayerController GetPlayerController(string name)
    {
        foreach(PlayerController player in players)
        {
            if(player.Info.Name == name)
            {
                return player;
            }
        }
        return null;
    }
    public PlayerController CurrentPlayer
    {
        get
        {
            return currentPlayer;
        }
    }

    private void Awake()
    {
        players = new List<PlayerController>();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsCurrentPlayerController(PlayerController pc)
    {
        if(pc == currentPlayer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    Dictionary<ResourceData.ResourceType, Resource> InitResources()
    {
        Dictionary<ResourceData.ResourceType, Resource> resourcesList = new Dictionary<ResourceData.ResourceType, Resource>();

        Resource matter = new Resource(0,0,
            GameManager.Instance.EventHub.SignalMatterCurrentChanged,
            GameManager.Instance.EventHub.SignalMatterMaxChanged,
            GameManager.Instance.EventHub.SignalMatterCriticallyLow,
            GameManager.Instance.EventHub.SignalMatterMaxed,
            0.1f, 0.9f
            );
        Resource energy = new Resource(0,0,
            GameManager.Instance.EventHub.SignalEnergyCurrentChanged,
            GameManager.Instance.EventHub.SignalEnergyMaxChanged,
            GameManager.Instance.EventHub.SignalEnergyCriticallyLow,
            GameManager.Instance.EventHub.SignalEnergyMaxed,
            0.1f, 0.9f
            );

        resourcesList.Add(ResourceData.ResourceType.Matter, matter);
        resourcesList.Add(ResourceData.ResourceType.Energy, energy);

        return resourcesList;
    }

    public void Init(List<Vector3> startPositions,  List<PlayerInfo> playersInfo)
    {
        FindObjectOfType<ResourcePanelController>().Init();         //TODO: FIND A WAY TO AVOID THIS HACK - order problem

        this.startPositions = startPositions;

        if(!playersGO)
        {
            playersGO = new GameObject("Players");
        }
        

        Dictionary<ResourceData.ResourceType, Resource> resourcesList = InitResources();

        uint playerID = 0;
        foreach (PlayerInfo player in playersInfo)
        {
            GameObject playerGO = new GameObject(player.Name);
            playerGO.transform.parent = playersGO.transform;
            PlayerController playerController = playerGO.AddComponent<PlayerController>();

            bool isPlayer = false;
            if (player.Type == ResourceData.PlayerType.Player)
            {
                currentPlayer = playerController;
                isPlayer = true;
            }

            playerController.Init(player, resourcesList, playerID++, isPlayer);
            players.Add(playerController);
        }

        if(players != null)
        {
            foreach (PlayerController player in players)
            {
                if(player.Info.Race != ResourceData.Races.Neutral && player.Info.StartPosition != PlayerInfo.NO_SPAWN_POINT)
                {
                    Unit mainBuilding = Instantiate<Unit>(ResourceData.Instance.mainBuildingRace1Prefab);

                    ResourceData.ObjectInfo objInfo = ResourceData.Instance.unitsInfoList.Find(obj => (obj.Name == "BaseCore" && obj.Race == player.Info.Race));
                    if (objInfo != null)
                    {
                        player.Matter.MaxIncrease((int)objInfo.MatterMaxIncrease);
                        player.Energy.MaxIncrease((int)objInfo.EnergyMaxIncrease);
                        player.Matter.Add(20000);
                        player.Energy.Add(player.Energy.Max);
                    }
                    mainBuilding.Init(player);
                    mainBuilding.transform.position = startPositions[player.StartPosition];
                }
            }
        }
        else
        {
            Debug.LogWarning("No players loaded by lvl manager.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempStart : MonoBehaviour
{
    PlayerController player1;
    PlayerController player2;
    PlayerController neutral;

    [SerializeField] ResourceData.Races player1Race;
    [SerializeField] Color player1Color = Color.blue;
    [SerializeField] int player1SpawnPoint = 0;
    [SerializeField] int player1AddMatter = 1000;
    [SerializeField] int player1AddMatterMax = 5000;
    [SerializeField] int player1AddEnergy = 500;
    [SerializeField] int player1AddEnergyMax = 500;

    [SerializeField] ResourceData.Races player2Race;
    [SerializeField] Color player2Color = Color.red;
    [SerializeField] int player2SpawnPoint = 1;
    [SerializeField] int player2AddMatter = 5000;
    [SerializeField] int player2AddMatterMax = 10000;
    [SerializeField] int player2AddEnergy = 1000;
    [SerializeField] int player2AddEnergyMax = 1000;

    [SerializeField] Color neutralColor = Color.white;

    [SerializeField] List<Unit> ObjectsToInitTeam1;
    [SerializeField] List<Unit> ObjectsToInitTeam2;
    [SerializeField] List<Unit> ObjectsToInitNeutral;

    // Use this for initialization
    void Start()
    {
        LevelManager lvlMgr = GameManager.Instance.LevelManager;

        List<PlayerInfo> playersInfoList = new List<PlayerInfo>();
        playersInfoList.Add(new PlayerInfo(ResourceData.Teams.Team1, player1Race, player1Color, player1SpawnPoint, ResourceData.PlayerType.Player, "Player 1"));
        playersInfoList.Add(new PlayerInfo(ResourceData.Teams.Team2, player2Race, player2Color, player2SpawnPoint, ResourceData.PlayerType.AIEasy, "Player 2"));
        playersInfoList.Add(new PlayerInfo(ResourceData.Teams.Neutral, ResourceData.Races.Neutral, neutralColor, PlayerInfo.NO_SPAWN_POINT, ResourceData.PlayerType.AIEasy, "Neutral"));

        lvlMgr.Init(ResourceData.Instance.levels[0].spawnPositions, playersInfoList);

        player1 = lvlMgr.GetPlayerController("Player 1");
        player2 = lvlMgr.GetPlayerController("Player 2");
        neutral = lvlMgr.GetPlayerController("Neutral");

        player2.Matter.MaxIncrease( player2AddMatterMax);
        player2.Matter.Add(         player2AddMatter);
        player2.Energy.MaxIncrease( player2AddEnergyMax);
        player2.Energy.Add(         player2AddEnergy);
        player1.Matter.MaxIncrease( player1AddMatterMax);
        player1.Matter.Add(         player1AddMatter);
        player1.Energy.MaxIncrease( player1AddEnergyMax);
        player1.Energy.Add(         player1AddEnergy);

        foreach (Unit unit in ObjectsToInitTeam1)
        {
            InitPlayer(unit, player1);
        }
        foreach (Unit unit in ObjectsToInitTeam2)
        {
            InitPlayer(unit, player2);
        }

        foreach (Unit unit in ObjectsToInitNeutral)
        {
            InitPlayer(unit, neutral);
        }


    }

    void InitPlayer(Unit unit, PlayerController player)
    {
        if (unit)
        {
            unit.Init(player);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

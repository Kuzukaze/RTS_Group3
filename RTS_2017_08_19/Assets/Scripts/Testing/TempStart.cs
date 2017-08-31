using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempStart : MonoBehaviour
{
    PlayerController player1;
    PlayerController player2;
    PlayerController neutral;

    [SerializeField] TeamInfo.Races player1Race;
    [SerializeField] Color player1Color = Color.blue;
    [SerializeField] int player1SpawnPoint = 1;

    [SerializeField] TeamInfo.Races player2Race;
    [SerializeField] Color player2Color = Color.red;
    [SerializeField] int player2SpawnPoint = 2;

    [SerializeField] Color neutralColor = Color.white;

    [SerializeField] List<Unit> ObjectsToInitTeam1;
    [SerializeField] List<Unit> ObjectsToInitTeam2;
    [SerializeField] List<Unit> ObjectsToInitNeutral;

    // Use this for initialization
    void Start()
    {
        player1 = new PlayerController();
<<<<<<< HEAD
        player1.Init(new TeamInfo(TeamInfo.Teams.Team1, player1Race, player1Color), player1SpawnPoint, "Player 1");

        player2 = new PlayerController();
        player2.Init(new TeamInfo(TeamInfo.Teams.Team2, player2Race, player2Color), player2SpawnPoint, "Player 2");

        neutral = new PlayerController();
        neutral.Init(new TeamInfo(TeamInfo.Teams.Neutral, TeamInfo.Races.Neutral, neutralColor), 0, "Neutral");
=======
        player1.Init(new TeamInfo(TeamInfo.Teams.Team1, player1Race, player1Color), player1SpawnPoint);

        player2 = new PlayerController();
        player2.Init(new TeamInfo(TeamInfo.Teams.Team2, player2Race, player2Color), player2SpawnPoint);

        neutral = new PlayerController();
        neutral.Init(new TeamInfo(TeamInfo.Teams.Neutral, TeamInfo.Races.Neutral, neutralColor), 0);
>>>>>>> master

        foreach(Unit unit in ObjectsToInitTeam1)
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

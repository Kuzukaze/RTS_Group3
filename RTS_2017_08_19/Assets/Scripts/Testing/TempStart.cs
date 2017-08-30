using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempStart : MonoBehaviour
{
    PlayerController player1;
    PlayerController neutral;

    // Use this for initialization
    void Start()
    {
        player1 = new PlayerController();
        player1.Init(new TeamInfo(TeamInfo.Teams.Team1, TeamInfo.Races.Race1, Color.blue), 1);

        neutral = new PlayerController();
        neutral.Init(new TeamInfo(TeamInfo.Teams.Neutral, TeamInfo.Races.Neutral, Color.white), 1);

        GameObject.Find("BaseCore").GetComponent<Unit>().Init(player1);
        GameObject.Find("Zergling").GetComponent<Unit>().Init(neutral);

        Unit unit = GameObject.Find("Engineer").GetComponent<Unit>();
        if (unit)
        {
            unit.Init(player1);
        }
        unit = GameObject.Find("Marine").GetComponent<Unit>();
        if (unit)
        {
            unit.Init(player1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeamInfo
{
    [SerializeField] private Teams team;
    [SerializeField] private Races race;
    [SerializeField] private Color color;

    public Color Color
    {
        get
        {
            return color;
        }
    }
    public Teams Team
    {
        get
        {
            return team;
        }
    }
    public Races Race
    {
        get
        {
            return race;
        }
    }


    public TeamInfo(Teams team, Races race, Color color)
    {
        this.team = team;
        this.race = race;   
        this.color = color;
    }
    

    public enum Teams
    {
        Team1 = 1,
        Team2 = 2,
        Team3 = 3,
        Team4 = 4,
        Team5 = 5,
        Team6 = 6,
        Team7 = 7,
        Team8 = 8,
        Team9 = 9,
        Team10 = 10,
        Team11 = 11,
        Team12 = 12,
        Team13 = 13,
        Team14 = 14,
        Team15 = 15,
        Team16 = 16,
        Neutral = 17
    }

    public enum Races
    {
        Race1 = 1,
        Race2 = 2,
        Neutral = 3
    }
}

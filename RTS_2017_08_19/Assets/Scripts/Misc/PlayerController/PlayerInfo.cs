using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    [SerializeField] private ResourceData.Teams team;
    [SerializeField] private ResourceData.Races race;
    [SerializeField] private Color color;
    [SerializeField] private int startPosition;
    [SerializeField] private ResourceData.PlayerType playerType;
    [SerializeField] private string playerName;

    public Color Color
    {
        get
        {
            return color;
        }
    }
    public ResourceData.Teams Team
    {
        get
        {
            return team;
        }
    }
    public ResourceData.Races Race
    {
        get
        {
            return race;
        }
    }
    public int StartPosition
    {
        get
        {
            return startPosition;
        }
    }
    public string Name
    {
        get
        {
            return playerName;
        }
    }
    public ResourceData.PlayerType Type
    {
        get
        {
            return playerType;
        }
    }

    public const int NO_SPAWN_POINT = -1;

    public PlayerInfo(ResourceData.Teams team, ResourceData.Races race, Color color, int startPosition, ResourceData.PlayerType playerType, string playerName)
    {
        this.team = team;
        this.race = race;
        this.color = color;
        this.startPosition = startPosition;
        this.playerType = playerType;
        this.playerName = playerName;
    }
   
}

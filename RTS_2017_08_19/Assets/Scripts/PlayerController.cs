using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController 
{
    [SerializeField] private string playerName = "...";
    [SerializeField] private TeamInfo team;
    [SerializeField] private int startPosition;
    private bool isInit = false;

    public TeamInfo Team
    {
        get
        {
            return team;
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
    

    public void Init(TeamInfo team, int startPosition, string name)
    {
        this.team = team;
        this.startPosition = startPosition;
        this.playerName = name;
        isInit = true;
    }
    
}

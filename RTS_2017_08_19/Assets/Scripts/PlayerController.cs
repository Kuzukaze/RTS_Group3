using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TeamInfo team;
    private int startPosition;
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


    // Use this for initialization
    void Start()
    {
        
    }

    public void Init(TeamInfo team, int startPosition)
    {
        this.team = team;
        this.startPosition = startPosition;
        isInit = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

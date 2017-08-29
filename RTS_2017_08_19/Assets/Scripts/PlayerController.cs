using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TeamInfo team;
    private bool isInit = false;

    public TeamInfo Team
    {
        get
        {
            return team;
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    public void Init(TeamInfo team)
    {
        this.team = team;
        isInit = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

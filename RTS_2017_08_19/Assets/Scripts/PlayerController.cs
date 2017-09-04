using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    private EventHub eventHub;
    private LevelManager levelManager;
    private bool isPlayer = false;
    [SerializeField] private uint id;

    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private int score = 0;
    [SerializeField] private Dictionary<ResourceData.ResourceType, Resource> resources;

    public PlayerInfo Info
    {
        get
        {
            return playerInfo;
        }
    }

    public Resource Matter
    {
        get
        {
            return resources[ResourceData.ResourceType.Matter];
        }
    }
    public Resource Energy
    {
        get
        {
            return resources[ResourceData.ResourceType.Energy];
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
    }

    private bool isInit = false;

    public ResourceData.Teams Team
    {
        get
        {
            return playerInfo.Team;
        }
    }
    public int StartPosition
    {
        get
        {
            return playerInfo.StartPosition;
        }
    }
    public string Name
    {
        get
        {
            return playerInfo.Name;
        }
    }
    public bool IsPlayer
    {
        get
        {
            return isPlayer;
        }
    }
    public uint ID
    {
        get
        {
            return id;
        }
    }



    public void Init(PlayerInfo playerInfo, Dictionary<ResourceData.ResourceType, Resource> resourcesList, uint id, bool isPlayer)
    {
        resources = new Dictionary<ResourceData.ResourceType, Resource>();
        foreach(KeyValuePair<ResourceData.ResourceType, Resource> resourcePair in resourcesList)
        {
            Resource resource = new Resource(resourcePair.Value);
            resources.Add(resourcePair.Key, resource);
            if(isPlayer)
            {
                resource.SetIsInvoker(true);
            }
        }

        levelManager = GameManager.Instance.LevelManager;

        this.id = id;
        this.isPlayer = isPlayer;
        this.playerInfo = playerInfo;
        this.score = 0;
        isInit = true;
    }

    private void Update()
    {
        //TODO: debug lines
        if (isPlayer)
        {
            if (Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.M) && Input.GetKeyDown(KeyCode.Equals))
            {
                Matter.Add(100);
            }
            if (Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.M) && Input.GetKeyDown(KeyCode.Minus))
            {
                Matter.Use(100);
            }
            if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.M) && Input.GetKeyDown(KeyCode.Equals))
            {
                Matter.MaxIncrease(1000);
            }
            if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.M) && Input.GetKeyDown(KeyCode.Minus))
            {
                Matter.MaxDecrease(1000);
            }

            if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.M) && Input.GetKeyDown(KeyCode.Equals))
            {
                Energy.Add(10);
            }
            if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.M) && Input.GetKeyDown(KeyCode.Minus))
            {
                Energy.Use(10);
            }
            if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.M) && Input.GetKeyDown(KeyCode.Equals))
            {
                Energy.MaxIncrease(100);
            }
            if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.M) && Input.GetKeyDown(KeyCode.Minus))
            {
                Energy.MaxDecrease(100);
            }
        }
    }

    private void Start()
    {
        eventHub = FindObjectOfType<EventHub>();
        if(eventHub == null)
        {
            Debug.LogError("Error: EventHub in PlayerController (" + Info.Name + ") cannot be found");
        }
    }
}

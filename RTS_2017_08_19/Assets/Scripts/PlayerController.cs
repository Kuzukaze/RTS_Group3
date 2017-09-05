using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    private bool isInit = false;

    private EventHub eventHub;
    private LevelManager levelManager;

    [SerializeField] private bool isPlayer = false;
    [SerializeField] private uint id;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private int score = 0;

    private Dictionary<ResourceData.ResourceType, Resource> resources;
    private Dictionary<ResourceData.ResourceType, List<ResourceStorage>> resourceStorages;




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




    public Resource GetResourceByType(ResourceData.ResourceType type)
    {
        return resources[type];
    }
    public Vector3 GetClosestResourceStoragePosition(Vector3 position, ResourceData.ResourceType type)
    {
        return GetClosestResourceStorage(position, type).transform.position;
    }
    public ResourceStorage GetClosestResourceStorage(Vector3 position, ResourceData.ResourceType type)
    {
        ResourceStorage closest = resourceStorages[type][0];
        float minPathLen = float.MaxValue;

        foreach (ResourceStorage current in resourceStorages[type])
        {
            NavMeshPath navMeshPath = new NavMeshPath(); ;
            if(NavMesh.CalculatePath(position, current.transform.position, 1, navMeshPath))
            {
                float pathLen = 0;
                for(int i = 0; i < navMeshPath.corners.Length - 1; i++)
                {
                    pathLen += Vector3.Distance(navMeshPath.corners[i], navMeshPath.corners[i+1]);
                }
                if(minPathLen > pathLen)
                {
                    minPathLen = pathLen;
                    closest = current;
                }
            }
        }

        return closest;
    }
    public void AddResourceStorage(ResourceStorage storage, ResourceData.ResourceType type)
    {
        resourceStorages[type].Add(storage);
    }
    public void RemoveResourceStorage(ResourceStorage storage, ResourceData.ResourceType type)
    {
        resourceStorages[type].Remove(storage);
    }


    public void Init(PlayerInfo playerInfo, Dictionary<ResourceData.ResourceType, Resource> resourcesList, uint id, bool isPlayer)
    {
        resources = new Dictionary<ResourceData.ResourceType, Resource>();
        resourceStorages = new Dictionary<ResourceData.ResourceType, List<ResourceStorage>>();
        foreach(KeyValuePair<ResourceData.ResourceType, Resource> resourcePair in resourcesList)
        {
            Resource resource = new Resource(resourcePair.Value);
            resources.Add(resourcePair.Key, resource);
            resourceStorages.Add(resourcePair.Key, new List<ResourceStorage>());
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

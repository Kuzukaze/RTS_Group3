using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceData : MonoBehaviour
{
    private static ResourceData instance;
    private ResourceData()
    {

    }
    public static ResourceData Instance
    {
        get
        {
            if (instance == null)
                instance = new ResourceData();
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.transform.gameObject);
    }


    [SerializeField] public List<LevelInfo> levels;
    [SerializeField] public Unit mainBuildingRace1Prefab;
    [SerializeField] public Unit mainBuildingRace2Prefab;

    public List<ObjectInfo> unitsInfoList;


    [System.Serializable]
    public class LevelInfo
    {
        public string levelName;
        public Sprite levelPreview;
        public string levelDescription;

        public int playersMax;
        public Terrain levelTerrain;
        public Terrain levelTerrainMiniMap;
        public List<Vector3> spawnPositions;

        public string sceneName;
    }


    //------------
    //ui data
    //TODO: must be stored as external file to let make features as translation
    public static List<string> playerTypesList = new List<string>()
    {
        "Player",
        "Computer (easy)",
        "Computer (medium)",
        "Computer (hard)"
    };
    public static List<string> racesList = new List<string>()
    {
        "Race 1",
        "Race 2"
    };
    public static List<string> teamsList = new List<string>()
    {
        "Team 1",
        "Team 2",
        "Team 3",
        "Team 4",
        "Team 5",
        "Team 6",
        "Team 7",
        "Team 8",
        "Team 9",
        "Team 10",
        "Team 11",
        "Team 12",
        "Team 13",
        "Team 14",
        "Team 15",
        "Team 16"
    };
    public static List<string> spawnPositionsList = new List<string>()
    {
        "Position 1",
        "Position 2",
        "Position 3",
        "Position 4",
        "Position 5",
        "Position 6",
        "Position 7",
        "Position 8",
        "Position 9",
        "Position 10",
        "Position 11",
        "Position 12",
        "Position 13",
        "Position 14",
        "Position 15",
        "Position 16"
    };
    public static List<Color> playerColorsList = new List<Color>()
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.black,
        Color.cyan,
        Color.grey,
        Color.magenta
    };

    //------------
    //useful data

    public enum ResourceType
    {
        Matter = 0,
        Energy = 1
    }


    public enum cargoID
    {
        Matter = 0,
        Energy = 1
    }

    //------------
    //objects configs

    [System.Serializable]
    public class ObjectInfo
    {
        public string Name;
        public Races Race;
        public uint Health;
        public uint Id;
        public uint MatterCost;
        public uint EnergyCost;
        public uint MatterMaxIncrease;
        public uint EnergyMaxIncrease;
        public uint Speed;
        public uint Damage;
        public uint FireRate;
        public List<uint> ActionsIDList;
    }


    //------------
    //player configuration info
    public enum Teams
    {
        Team1 = 0,
        Team2 = 1,
        Team3 = 2,
        Team4 = 3,
        Team5 = 4,
        Team6 = 5,
        Team7 = 6,
        Team8 = 7,
        Team9 = 8,
        Team10 = 9,
        Team11 = 10,
        Team12 = 11,
        Team13 = 12,
        Team14 = 13,
        Team15 = 14,
        Team16 = 15,
        Neutral = 16
    }
    public enum Races
    {
        Race1 = 0,
        Race2 = 1,
        Neutral = 2
    }
    public enum PlayerType
    {
        Player = 0,
        AIEasy = 1,
        AIMeduim = 2,
        AIHard = 3
    }
}

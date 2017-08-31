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
}

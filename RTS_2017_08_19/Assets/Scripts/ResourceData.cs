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
}

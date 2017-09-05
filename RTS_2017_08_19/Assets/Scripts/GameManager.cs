using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string startScene;
    [SerializeField] private List<PlayerInfo> playersInfoList;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private EventHub eventHub;
    [SerializeField] ResourceData.LevelInfo currentLevel;

    private List<Selectable> selectableObjectsList;

    public List<Selectable> SelectableObjects
    {
        get
        {
            return selectableObjectsList;
        }
    }
    public void AddSelectableObject(Selectable selected)
    {
        selectableObjectsList.Add(selected);
    }
    public void RemoveSelectableObject(Selectable selected)
    {
        if (selected != null && selectableObjectsList != null)
        {
            if (selectableObjectsList != null)
            {
                selectableObjectsList.Remove(selected);
            }
        }
    }
    public void RemoveSelectableObjectByID(int id)
    {
        if (selectableObjectsList != null && selectableObjectsList[id] != null)
        {
            selectableObjectsList.RemoveAt(id);
        }
    }
    public void ClearSelectableObjectsList()
    {
        selectableObjectsList.Clear();
    }

    public LevelManager LevelManager
    {
        get
        {
            return levelManager;
        }
    }
    public EventHub EventHub
    {
        get
        {
            return eventHub;
        }
    }


    private bool isInit = false;
    private static GameManager instance;

    private GameManager()
    {

    }
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }
    }

 private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.transform.gameObject);

        selectableObjectsList = new List<Selectable>();
    }
    void Start()
    {
        Init();

        SceneManager.LoadScene(startScene);
    }
    void Init()
    {
        playersInfoList = new List<PlayerInfo>();
        isInit = true;
    }
    public bool IsInit()
    {
        return isInit;
    }
   
    void Update()
    {

    }
        
    public void SetStartScene(string startScene)      //(!) DebugFunction ONLY
    {
        this.startScene = startScene;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    

    public void CreateAIPlayer()
    {

    }

    public void LoadSkirmishLevelList(out List<ResourceData.LevelInfo> lvlList)
    {
        lvlList = new List<ResourceData.LevelInfo>();

        TemporaryLevelLoading(out lvlList);
    }

    //TODO: rewrite to init from dataFile
    private void TemporaryLevelLoading(out List<ResourceData.LevelInfo> lvlList)
    {
        ResourceData.LevelInfo lvl1 = ResourceData.Instance.levels[0] /*new ResourceData.LevelInfo()
        {
            levelDescription = "Test map 1x1",
            levelName = "level1"
        }*/;
        lvlList = new List<ResourceData.LevelInfo>();
        lvlList.Add(lvl1);

        for (int i = 2; i < 50; i++)
        {
            ResourceData.LevelInfo lvl = new ResourceData.LevelInfo()
            {
                levelDescription = "Test stub _ " + i + ". Don't select!",
                levelName = "level" + i
            };
            lvlList.Add(lvl);
        }
    }
    
    public void LoadSkirmishLevel(ResourceData.LevelInfo lvl, List<PlayerInfo> playerControllerList)
    {
        currentLevel = lvl;

        playersInfoList = playerControllerList;// new List<PlayerInfo>();

        //foreach ( PlayerInfo pc in playerControllerList)
        //{
        //    TeamInfo ti = new TeamInfo(pc.Team.Team, pc.Team.Race, pc.Team.Color);


        //    PlayerInfo pController = new PlayerInfo(pc.Team.Team, pc.Team.Race, pc.Team.Color, pc.StartPosition, pc.Name);

        //    playersInfoList.Add(pController);

        //}

        //playersList = new List<PlayerController>(playerControllerList);
        SceneManager.LoadScene(lvl.sceneName);
    }

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex < 2)
        {
            return;
        }

        GameObject managers = GameObject.Find("Managers");
        if (!managers)
        { 
            managers = new GameObject("Managers");
            //levelManager = new LevelManager();
        }

        if(!managers.GetComponent<EventHub>())
        {
            eventHub = managers.AddComponent<EventHub>();
        }
        else
        {
            eventHub = managers.GetComponent<EventHub>();
        }

        levelManager = managers.AddComponent<LevelManager>();
        levelManager.Init(currentLevel.spawnPositions, playersInfoList);
    }
    

}

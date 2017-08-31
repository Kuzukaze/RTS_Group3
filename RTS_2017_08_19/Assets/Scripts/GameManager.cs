using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string startScene;
    [SerializeField] private List<PlayerController> playersList;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] ResourceData.LevelInfo currentLevel;

    private List<Selectable> selectableObjectsList;
    
    //TODO must be here? Or in Selection Manager...
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
        if(selected != null)
        {
            if (selectableObjectsList != null)
            {
                selectableObjectsList.Remove(selected);
            }
        }
    }
    public void RemoveSelectableObjectByID(int id)
    {
        if(selectableObjectsList != null && selectableObjectsList[id] != null)
        {
            selectableObjectsList.RemoveAt(id);
        }
    }
    public void ClearSelectableObjectsList()
    {
        selectableObjectsList.Clear();
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
    

    public PlayerController CreatePlayer(TeamInfo teamInfo, int startPosition)
    {
        PlayerController pc = new PlayerController();
        pc.Init(teamInfo, startPosition);

        return pc;
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

    public void LoadSkirmishLevel(ResourceData.LevelInfo lvl)
    {
        currentLevel = lvl;
        SceneManager.LoadScene(lvl.sceneName);
    }

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        List<TeamInfo> teamInfo = new List<TeamInfo>();                                                     //TODO: must be gui team params selection
        teamInfo.Add(new TeamInfo(TeamInfo.Teams.Team1, TeamInfo.Races.Race1, Color.blue));                 //TODO: must be gui team params selection
        teamInfo.Add(new TeamInfo(TeamInfo.Teams.Team2, TeamInfo.Races.Race1, Color.red));                  //TODO: must be gui team params selection

        playersList = new List<PlayerController>();
        for (int i = 0; i < currentLevel.playersMax; i++)
        {
            playersList.Add(CreatePlayer(teamInfo[i], i));
        }

        levelManager = new LevelManager();
        levelManager.Init(currentLevel.spawnPositions, playersList);
    }

}

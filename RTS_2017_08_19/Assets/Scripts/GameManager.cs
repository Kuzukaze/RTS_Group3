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
        if(selected != null && selectableObjectsList != null)
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
        //TEST
        if(Input.GetKeyUp(KeyCode.Q))
        {
            foreach(PlayerController player in playersList)
            {
                Debug.Log("Player name: " + player.Name);
            }
        }
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
    

    public PlayerController CreatePlayer(TeamInfo teamInfo, int startPosition, string name)
    {
        PlayerController pc = new PlayerController();
        pc.Init(teamInfo, startPosition, name);

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

    [SerializeField] private List<ResourceData.LevelInfo> testLvlsList;

    public void LoadSkirmishLevel(ResourceData.LevelInfo lvl, List<PlayerController> playerControllerList)
    {
        testLvlsList = new List<ResourceData.LevelInfo>();
        testLvlsList.Add(lvl);
        testLvlsList.Add(lvl);
        testLvlsList.Add(lvl);
        testLvlsList.Add(lvl);

        currentLevel = lvl;

        playersList = new List<PlayerController>();

        foreach ( PlayerController pc in playerControllerList)
        {
            TeamInfo ti = new TeamInfo(pc.Team.Team, pc.Team.Race, pc.Team.Color);


            PlayerController pController = new PlayerController();
            pController.Init(ti, pc.StartPosition, pc.Name);

            playersList.Add(pController);

        }

        //playersList = new List<PlayerController>(playerControllerList);
        SceneManager.LoadScene(lvl.sceneName);
    }

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        GameObject managers = GameObject.Find("Managers");
        if (managers)
        {
            levelManager = managers.AddComponent<LevelManager>();
        }
        else
        {
            levelManager = new LevelManager();
        }

        levelManager.Init(currentLevel.spawnPositions, playersList);
    }

}

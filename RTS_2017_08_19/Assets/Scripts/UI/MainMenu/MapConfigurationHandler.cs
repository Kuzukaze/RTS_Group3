using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapConfigurationHandler : MonoBehaviour
{
    [SerializeField] private Transform contentObject;

    [SerializeField] private PlayerPanelController playerPanelPrefab;
    [SerializeField] private Sprite colorsBaseSprite;
    
    [SerializeField] private Image mapImage;
    [SerializeField] private Text mapDescription;
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonStartGame;

    private MainMenuCanvasHandler mainMenuHandler;
    private List<PlayerPanelController> playerPanelControllerList;
    private List<PlayerInfo> playersInfoList;
    private ResourceData.LevelInfo currentLevel;

    // Use this for initialization
    void Start()
    {
        mainMenuHandler = this.transform.parent.GetComponent<MainMenuCanvasHandler>();

        buttonBack.onClick.AddListener(OnButtonBack);
        buttonStartGame.onClick.AddListener(OnButtonStartGame);

        playerPanelControllerList = new List<PlayerPanelController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadMapConfig(ResourceData.LevelInfo lvl)
    {
        currentLevel = lvl;

        List<Sprite> colors = new List<Sprite>();
        foreach(Color color in ResourceData.playerColorsList)
        {
            var texture = new Texture2D(1, 1); 
            texture.SetPixel(0, 0, color); 
            texture.Apply(); 
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            
            colors.Add(sprite);
        }
        

        foreach (Transform child in contentObject)
        {
            Destroy(child.gameObject);
        }
        playerPanelControllerList.Clear();

        for (int i = 0; i < lvl.playersMax; i++)
        {
            PlayerPanelController playerPanelController = Instantiate<PlayerPanelController>(playerPanelPrefab, contentObject);
            playerPanelController.InitPlayerPanel(ResourceData.playerTypesList, ResourceData.racesList, ResourceData.teamsList/*.GetRange(0, lvl.spawnPositions.Count)*/, ResourceData.spawnPositionsList.GetRange(0, lvl.spawnPositions.Count), colors);
            playerPanelControllerList.Add(playerPanelController);
        }
        
        mapImage.sprite = lvl.levelPreview;
        mapDescription.text = lvl.levelDescription;
    }


    private void OnButtonBack()
    {
        mainMenuHandler.HideMapConfiguration();
    }

    private void OnButtonStartGame()
    {
        playersInfoList = new List<PlayerInfo>();

        foreach(PlayerPanelController panelPlayer in playerPanelControllerList)
        {
            playersInfoList.Add(panelPlayer.GetPlayerInfo());
        }

        GameManager.Instance.LoadSkirmishLevel(currentLevel, playersInfoList);
    }
}

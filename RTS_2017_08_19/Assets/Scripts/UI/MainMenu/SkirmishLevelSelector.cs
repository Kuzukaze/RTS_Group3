using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkirmishLevelSelector : MonoBehaviour
{
    [SerializeField] private Button buttonAccept;
    [SerializeField] private Button buttonCancel;

    [SerializeField] private LevelSelector buttonLevelPrefab;
    [SerializeField] private Transform contentObject;
    [SerializeField] private Image mapImage;
    [SerializeField] private Text mapDescription;

    private List<ResourceData.LevelInfo> levelList;
    private int selectedLevel = -1;

    // Use this for initialization
    private void Awake()
    {
        levelList = new List<ResourceData.LevelInfo>();
    }

    void Start()
    {
        buttonAccept.onClick.AddListener(OnButtonAccept);
        buttonCancel.onClick.AddListener(OnButtonCancel);
    }

    void OnEnable()
    {
        levelList.Clear();
        GameManager.Instance.LoadSkirmishLevelList(out levelList);

        UpdateData();
    }

    // Update is called once per frame
    void Update()   
    {

    }

    void UpdateData()
    {
        for(int i =0; i < levelList.Count; i++)
        {
            LevelSelector lvl = Instantiate<LevelSelector>(buttonLevelPrefab, contentObject);
            lvl.GetComponentInChildren<Text>().text = levelList[i].levelName;
            lvl.Init(this, i);
        }

        if(levelList.Count > 0)
        {
            selectedLevel = 0;
            mapImage.sprite = levelList[0].levelPreview;
            mapDescription.text = levelList[0].levelDescription;
        }
        else
        {
            selectedLevel = -1;
        }
    }

    public void SelectLevel(int number)
    {
        mapImage.sprite = levelList[number].levelPreview;
        mapDescription.text = levelList[number].levelDescription;
        selectedLevel = number;
    }

    private void OnButtonAccept()
    {
        //todo: here must be nice panel with additional skirmish config:
        //      -players (team & colors)
        //      -difficulty
        //      -victory condition & etc

        GameManager.Instance.LoadSkirmishLevel(levelList[selectedLevel]);
    }

    private void OnButtonCancel()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

}

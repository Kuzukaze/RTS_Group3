using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    private SkirmishLevelSelector skirmishLevelSelector;
    private int number = -1;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => { skirmishLevelSelector.SelectLevel(number); });
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public void Init(SkirmishLevelSelector skirmishLevelSelevtor, int number)
    {
        this.skirmishLevelSelector = skirmishLevelSelevtor;
        this.number = number;
    }
}

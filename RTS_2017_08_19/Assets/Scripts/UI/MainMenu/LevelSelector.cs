using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    private SkirmishLevelSelector skirmishLevelSelevtor;
    private int number = -1;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => { skirmishLevelSelevtor.SelectLevel(number); });
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public void Init(SkirmishLevelSelector skirmishLevelSelevtor, int number)
    {
        this.skirmishLevelSelevtor = skirmishLevelSelevtor;
        this.number = number;
    }
}

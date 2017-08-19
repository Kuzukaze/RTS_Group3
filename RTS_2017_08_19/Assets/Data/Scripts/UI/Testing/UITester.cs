using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITester : MonoBehaviour {

    //this tests the action panel manager

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private Sprite infoPicToAdd;

    [SerializeField]
    private string infoTextToAdd;

    //public Sprite[] pics;
    [SerializeField]
    private List<BaseAction> actions;

    private int i = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("n"))
        { //adding placeholder actions
            uiManager.AddAction(actions[i]);
            i++;
            i = i % actions.Count;
        }

        if (Input.GetKeyDown("m"))
            uiManager.UnselectObject();
        
        if (Input.GetKeyDown("b"))
            uiManager.UnselectAction();
        
        if (Input.GetKeyDown("v"))
            uiManager.SetInfoPic(infoPicToAdd);
        
        if (Input.GetKeyDown("c"))
            uiManager.SetInfoText(infoTextToAdd);
	}
}

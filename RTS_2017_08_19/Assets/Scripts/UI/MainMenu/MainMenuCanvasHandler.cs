using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasHandler : MonoBehaviour
{
    [SerializeField] private Button ButtonStartCampaign;
    [SerializeField] private Button ButtonStartSkirmish;
    [SerializeField] private Button ButtonStartMultiplayer;
    [SerializeField] private Button ButtonOptions;
    [SerializeField] private Button ButtonExtras;
    [SerializeField] private Button ButtonQuit;

    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject SkirmishPanel;

    // Use this for initialization
    void Start()
    {
        ButtonStartCampaign.onClick.AddListener(OnButtonStartCampaign);
        ButtonStartSkirmish.onClick.AddListener(OnButtonStartSkirmish);
        ButtonStartMultiplayer.onClick.AddListener(OnButtonStartMultiplayer);
        ButtonOptions.onClick.AddListener(OnButtonOptions);
        ButtonExtras.onClick.AddListener(OnButtonExtras);
        ButtonQuit.onClick.AddListener(OnButtonQuit);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnButtonStartCampaign()
    {
        Debug.Log("Started Campaign mode");
    }

    private void OnButtonStartSkirmish()
    {
        Debug.Log("Started Skirmish mode");

        SkirmishPanel.SetActive(!SkirmishPanel.activeSelf);
    }

    private void OnButtonStartMultiplayer()
    {
        Debug.Log("Started Multiplayer mode");
    }

    private void OnButtonOptions()
    {
        Debug.Log("Show options menu");
    }

    private void OnButtonExtras()
    {
        Debug.Log("Show extras menu");
    }

    private void OnButtonQuit()
    {
        Application.Quit();
    }
}

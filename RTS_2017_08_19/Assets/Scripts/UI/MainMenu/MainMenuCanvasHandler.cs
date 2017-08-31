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
    [SerializeField] private SkirmishLevelSelector SkirmishPanel;
    [SerializeField] private MapConfigurationHandler MapConfigurationPanel;

    public Animator animator;
    [SerializeField] private bool isAnimationPlayed = false;

    public void AnimationPlayed()
    {
        isAnimationPlayed = true;
    }
    public void AnimationStop() //called by animationEvents
    {
        isAnimationPlayed = false;
    }

    // Use this for initialization
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();

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

    
    public void ShowSkirmish()
    {
        AnimationPlayed();
        animator.SetBool("skirmishMenu", true);
    }
    public void HideSkirmish()
    {
        AnimationPlayed();
        animator.SetBool("skirmishMenu", false);
    }
    public void ShowMapConfiguration()
    {
        AnimationPlayed();
        animator.SetBool("menuConfiguration", true);
        MapConfigurationPanel.LoadMapConfig(SkirmishPanel.GetSelectedLevel());
    }
    public void HideMapConfiguration()
    {
        AnimationPlayed();
        animator.SetBool("menuConfiguration", false);
    }


    private void OnButtonStartCampaign()
    {
        if(!isAnimationPlayed)
        {
            Debug.Log("Started Campaign mode");
        }
    }

    private void OnButtonStartSkirmish()
    {
        if (!isAnimationPlayed)
        {
            Debug.Log("Started Skirmish mode");
            ShowSkirmish();
        }
    }

    private void OnButtonStartMultiplayer()
    {
        if (!isAnimationPlayed)
        {
            Debug.Log("Started Multiplayer mode");
        }
    }

    private void OnButtonOptions()
    {
        if (!isAnimationPlayed)
        {
            Debug.Log("Show options menu");
        }
    }

    private void OnButtonExtras()
    {
        if (!isAnimationPlayed)
        {
            Debug.Log("Show extras menu");
        }
    }

    private void OnButtonQuit()
    {
        if (!isAnimationPlayed)
        {
            Application.Quit();
        }
    }
}

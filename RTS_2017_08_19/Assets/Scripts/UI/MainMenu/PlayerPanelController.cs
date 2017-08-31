using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelController : MonoBehaviour
{
    [SerializeField] private Dropdown playerDropdown;
    [SerializeField] private Dropdown raceDropdown;
    [SerializeField] private Dropdown teamDropdown;
    [SerializeField] private Dropdown spawnPositionDropdown;
    [SerializeField] private Dropdown colorDropdown;

    [SerializeField] private Color color;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitPlayerPanel(List<string> players, List<string> races, List<string> teams, List<string> positions, List<Sprite> colors)
    {
        InitDropdown(playerDropdown, players);
        InitDropdown(raceDropdown, races);
        InitDropdown(teamDropdown, teams);
        InitDropdown(spawnPositionDropdown, positions);
        InitDropdown(colorDropdown, colors);
    }

    void InitDropdown(Dropdown dropdown, List<string> insertion)
    {
        dropdown.ClearOptions();
        
        foreach (var value in insertion)
        {
            dropdown.options.Add(new Dropdown.OptionData(value));
        }
        dropdown.captionText.text = insertion[0];
    }

    void InitDropdown(Dropdown dropdown, List<Sprite> insertion)
    {
        dropdown.ClearOptions();

        foreach (var value in insertion)
        {
            dropdown.options.Add(new Dropdown.OptionData(value));
        }
        dropdown.captionImage.sprite = insertion[0];
        dropdown.captionImage.enabled = true;
    }

    private int posInList(string str, List<string> list)
    {
        return list.FindIndex((s) => s == str);
    }

        //TODO: too many hacks - rewrite normally
    public PlayerController GetPlayerController()
    {
        PlayerController pc = new PlayerController();

        if(playerDropdown.captionText.text.Contains("Player"))
        {
            //create Player
        }
        else
        {
            //create AI
        }


        TeamInfo teamInfo = new TeamInfo(   (TeamInfo.Teams)posInList(teamDropdown.captionText.text, ResourceData.teamsList),
                                            (TeamInfo.Races)posInList(raceDropdown.captionText.text, ResourceData.racesList), 
                                            colorDropdown.captionImage.sprite.texture.GetPixel(0,0));
        

        return GameManager.Instance.CreatePlayer(teamInfo, posInList(spawnPositionDropdown.captionText.text, ResourceData.spawnPositionsList), playerDropdown.captionText.text);
    }
}

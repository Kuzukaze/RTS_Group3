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

    //TODO: too many hacks => rewrite | add to UI player name input
    public PlayerInfo GetPlayerInfo()
    {
        ResourceData.Teams team             = (ResourceData.Teams)posInList(teamDropdown.captionText.text, ResourceData.teamsList);
        ResourceData.Races race             = (ResourceData.Races)posInList(raceDropdown.captionText.text, ResourceData.racesList);
        Color color                         = colorDropdown.captionImage.sprite.texture.GetPixel(0, 0);
        int startPosition                   = posInList(spawnPositionDropdown.captionText.text, ResourceData.spawnPositionsList);
        ResourceData.PlayerType playerType  = (ResourceData.PlayerType)posInList(playerDropdown.captionText.text, ResourceData.playerTypesList);

        PlayerInfo player = new PlayerInfo(team, race, color, startPosition, playerType, playerDropdown.captionText.text);
        
        return player;
    }
}

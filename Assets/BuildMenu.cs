using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour {

    private MenuSounds menuSounds;
    private BuildMainMenu buildMainMenu;    
    
    public static MenuState MenuState { get; set; } = MenuState.NONE;
    
    private void Awake()
    {
        menuSounds = new MenuSounds();
        buildMainMenu = new BuildMainMenu(menuSounds);
    }
    
}

public enum MenuState
{
    NONE,
    MAINMENU,
    PAUSEMENU,
    OPTIONSMENU,
    LOAD,
    SAVE,
}
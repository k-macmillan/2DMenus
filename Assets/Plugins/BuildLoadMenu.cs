using UnityEngine;

public class BuildSaveMenu : BaseMenu {

    // Helper classes
    private MenuSounds menuSounds;

    // Constants

    // Prefabs
    private const string strButtonPrefab = "Prefabs/SF Button";
    

    private const string strBack = "Back";
    private const float btnBuffer = 0.60f;
    private const int loadSlots = 10;

    private CanvasPanel parentMenu;

    private GameObject buttonPrefab;


    public BuildSaveMenu(MenuSounds MenuSounds, CanvasPanel ParentMenu)
    {
        eventTrigger = null;
        parentMenu = ParentMenu;
        menuSounds = MenuSounds;
        menuSounds.StartMenuSounds();
        canvasPanel = new CanvasPanel();

        buttonPrefab = Resources.Load<GameObject>(strButtonPrefab);

        LoadMenuLoad();
    }



    private void LoadMenuLoad()
    {

    }


    protected override void HandleMenuClicks(GameObject obj)
    {
        
    }

}

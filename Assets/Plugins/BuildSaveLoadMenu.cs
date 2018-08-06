using UnityEngine;

public class BuildSaveLoadMenu : BaseMenu {

    // Helper classes
    private MenuSounds menuSounds;

    // Constants

    // Prefabs
    private const string strButtonPrefab = "Prefabs/SF Button";
    private const string strSaveLoadPrefab = "Prefabs/SaveLoadPanel";


    private const string strBack = "Back";
    private const float btnBuffer = 0.60f;
    //private const float btnBuffer = 0.70f;

    private const int saveLoadSlots = 3;

    private CanvasPanel parentMenu;

    private GameObject buttonPrefab;
    private GameObject saveLoadPrefab;

    public BuildSaveLoadMenu(MenuSounds MenuSounds, CanvasPanel ParentMenu)
    {
        eventTrigger = null;
        parentMenu = ParentMenu;
        menuSounds = MenuSounds;
        menuSounds.StartMenuSounds();
        canvasPanel = new CanvasPanel();

        buttonPrefab = Resources.Load<GameObject>(strButtonPrefab);
        saveLoadPrefab = Resources.Load<GameObject>(strSaveLoadPrefab);

        LoadSaveLoadMenu();
    }



    private void LoadSaveLoadMenu()
    {
        RectTransform panelRect = canvasPanel.MenuPanel.GetComponent<RectTransform>();
        RectTransform saveLoadRect = saveLoadPrefab.GetComponent<RectTransform>();
        RectTransform btnRect = buttonPrefab.GetComponent<RectTransform>();

        float saveLoadHeight = saveLoadRect.rect.height;
        float btnHeight = btnRect.rect.height;
        float diffWidth = 0;// Screen.width / 2.0f ;
        float diffHeight = (Screen.height - (saveLoadHeight * saveLoadSlots  + btnHeight)) / 2.0f;

        panelRect.offsetMin = new Vector2(diffWidth, diffHeight);
        panelRect.offsetMax = new Vector2(-diffWidth, -diffHeight);

        Vector3 objPosition = canvasPanel.MenuPanel.transform.position;
        objPosition.y += (saveLoadHeight * saveLoadSlots + btnHeight * btnBuffer) / 2.0f;
        Vector3 offset = new Vector3(0.0f, -(saveLoadHeight - btnHeight * btnBuffer) / 2.0f, 0.0f);
        

        InstantiateSaveLoad(saveLoadPrefab, "save0", canvasPanel.MenuPanel, ref objPosition, offset);
        offset.y = -saveLoadHeight;
        InstantiateSaveLoad(saveLoadPrefab, "save1", canvasPanel.MenuPanel, ref objPosition, offset);
        InstantiateSaveLoad(saveLoadPrefab, "save2", canvasPanel.MenuPanel, ref objPosition, offset);
        offset.y = -(saveLoadHeight / 2.0f + btnRect.rect.height / 2.0f);
        InstantiateButton(buttonPrefab, strBack, canvasPanel.MenuPanel, ref objPosition, offset);
    }

    private GameObject InstantiateSaveLoad(GameObject ObjToInstantiate, string DisplayText, GameObject Parent, ref Vector3 Position, Vector3 Offset = new Vector3())
    {
        Position += Offset;
        GameObject obj = Object.Instantiate(ObjToInstantiate, Parent.transform) as GameObject;
        obj.name = DisplayText;
        obj.transform.position = Position;
        //eventTrigger = obj.AddComponent<EventTrigger>();
        //AddEventTrigger(OnPointerEnter, EventTriggerType.PointerEnter);
        return obj;
    }


    protected override void HandleMenuClicks(GameObject obj)
    {
        
    }

}

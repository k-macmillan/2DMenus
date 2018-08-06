using UnityEngine;
using UnityEngine.UI;

public class BuildSaveLoadMenu : BaseMenu {

    // Helper classes
    private MenuSounds menuSounds;

    // Constants

    // Prefabs
    private const string strButtonPrefab = "Prefabs/SF Button";
    private const string strSaveLoadPrefab = "Prefabs/SaveLoadPanel";
    
    private const string strBack = "Back";
    private const string strSaveLoadButton = "SaveLoadGame";
    private const string strDeleteButton = "DeleteSave";
    private const float btnBuffer = 0.60f;
    private const float saveLoadBuffer = 0.90f;

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
        float diffWidth = 0;
        float diffHeight = (Screen.height - (saveLoadHeight * (saveLoadSlots + 1) * saveLoadBuffer)) / 2.0f;  // +1 for the button slot

        panelRect.offsetMin = new Vector2(diffWidth, diffHeight);
        panelRect.offsetMax = new Vector2(-diffWidth, -diffHeight);

        Vector3 objPosition = canvasPanel.MenuPanel.transform.position;
        objPosition.y += (saveLoadHeight * saveLoadSlots) / 2.0f - btnHeight * btnBuffer;
        Vector3 offset = new Vector3(0.0f, -saveLoadHeight, 0.0f);
        

        InstantiateSaveLoad(saveLoadPrefab, "0", canvasPanel.MenuPanel, ref objPosition);        
        InstantiateSaveLoad(saveLoadPrefab, "1", canvasPanel.MenuPanel, ref objPosition, offset);
        InstantiateSaveLoad(saveLoadPrefab, "2", canvasPanel.MenuPanel, ref objPosition, offset);

        offset.y = -(saveLoadHeight / 2.0f + btnRect.rect.height / 2.0f);
        InstantiateButton(buttonPrefab, strBack, canvasPanel.MenuPanel, ref objPosition, offset);
    }

    private GameObject InstantiateSaveLoad(GameObject ObjToInstantiate, string DisplayText, GameObject Parent, ref Vector3 Position, Vector3 Offset = new Vector3())
    {
        Position += Offset;
        GameObject obj = Object.Instantiate(ObjToInstantiate, Parent.transform) as GameObject;
        obj.name = DisplayText;
        obj.transform.position = Position;

        GameObject screenshot = obj.GetComponentInChildren<Transform>().Find("Screenshot").gameObject as GameObject;
        RawImage img = screenshot.GetComponentInChildren<RawImage>();
        Texture image = Resources.Load<Texture>("Images/example" + obj.name);
        img.texture = image;
        
        AssignListener(obj, strSaveLoadButton);
        AssignListener(obj, strDeleteButton);
        return obj;
    }

    private void AssignListener(GameObject Obj, string Name)
    {
        GameObject saveButton = Obj.GetComponentInChildren<Transform>().Find(Name).gameObject as GameObject;
        if (saveButton != null)
        {
            Button btn = saveButton.GetComponent<Button>() as Button;            
            btn.onClick.AddListener(delegate { HandleMenuClicks(saveButton); });
        }
    }


    protected override void HandleMenuClicks(GameObject obj)
    {
        switch (obj.name)
        {
            case strSaveLoadButton:
                Debug.Log("Game loaded!");
                canvasPanel.ShowCanvas(false);
                parentMenu.ShowCanvas(true);
                break;
            case strDeleteButton:
                Debug.Log("Game deleted!");
                GameObject parent = obj.GetComponentInParent<Transform>().parent.gameObject as GameObject;
                parent.SetActive(false);
                break;
            case strBack:
                canvasPanel.ShowCanvas(false);
                parentMenu.ShowCanvas(true);
                break;
            default:
                break;
        }
    }

}

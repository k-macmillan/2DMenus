using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuildMainMenu
{

    // Helper classes
    private MenuSounds menuSounds;
    private CanvasPanel canvasPanel;


    // Const variables
    private const string strMainMenu = "MainMenu";
    private const string strNewGame = "New Game";
    private const string strLoadGame = "Load";
    private const string strOptionsMenu = "Options";
    private const string strQuitGame = "Quit";
    private const string strButtonPrefab = "Prefabs/SF Button";
    private const int mainMenuButtonCount = 4;
    private const float btnBuffer = 0.60f;

    private GameObject buttonPrefab;
    private EventTrigger eventTrigger;


    /// <summary>
    /// Constructor. Sets up the sound and canvas/panel. Loads button prefab and then loads the buttons.
    /// </summary>
    /// <param name="MenuSounds">Reference to the MenuSounds object</param>
    public BuildMainMenu(MenuSounds MenuSounds)
    {
        eventTrigger = null;
        menuSounds = MenuSounds;
        menuSounds.StartMenuSounds();
        canvasPanel = new CanvasPanel();

        buttonPrefab = Resources.Load<GameObject>(strButtonPrefab);
        MainMenuLoad();
    }

    /// <summary>
    /// Loads the Main Menu and associated buttons.
    /// </summary>
    private void MainMenuLoad()
    {

        RectTransform panelRect = canvasPanel.MenuPanel.GetComponent<RectTransform>();
        RectTransform btnRect = buttonPrefab.GetComponent<RectTransform>();
        float diffWidth = Screen.width / 2.0f - btnRect.rect.width * btnBuffer;
        float diffHeight = Screen.height / 2.0f - btnRect.rect.height * btnBuffer * mainMenuButtonCount;

        panelRect.offsetMin = new Vector2(diffWidth, diffHeight);
        panelRect.offsetMax = new Vector2(-diffWidth, -diffHeight);

        Vector3 btnPosition = new Vector3
        {
            x = canvasPanel.MenuPanel.transform.position.x,
            y = canvasPanel.MenuPanel.transform.position.y + mainMenuButtonCount / 2.0f * btnRect.rect.height - btnRect.rect.height / 2.0f,
            z = canvasPanel.MenuPanel.transform.position.z
        };

        // Offset function y movement
        btnPosition.y += btnRect.rect.height;

        InstantiateButton(buttonPrefab, strNewGame, ref btnPosition, canvasPanel.MenuPanel);
        InstantiateButton(buttonPrefab, strLoadGame, ref btnPosition, canvasPanel.MenuPanel);
        InstantiateButton(buttonPrefab, strOptionsMenu, ref btnPosition, canvasPanel.MenuPanel);
        InstantiateButton(buttonPrefab, strQuitGame, ref btnPosition, canvasPanel.MenuPanel);
    }

    /// <summary>
    /// Used for instantiating and offsetting an Object.
    /// </summary>
    /// <param name="Obj">GameObject to be cloned</param>
    /// <param name="DisplayText">Display text</param>
    /// <param name="vec">Reference to offset vector</param>
    /// <param name="Parent">The parent of the created GameObject</param>
    /// <returns>The created GameObject</returns>
    private GameObject InstantiateButton(GameObject Obj, string DisplayText, ref Vector3 Vec, GameObject Parent = null)
    {
        GameObject obj = Object.Instantiate(Obj) as GameObject;
        obj.GetComponentInChildren<Text>().text = DisplayText;
        obj.name = DisplayText;
        obj.transform.SetParent(Parent.transform);
        Vec.y -= obj.GetComponent<RectTransform>().rect.height;
        obj.transform.position = new Vector3(Vec.x, Vec.y, Vec.z);

        Button btn = obj.GetComponent<Button>();
        eventTrigger = obj.AddComponent<EventTrigger>();
        AddEventTrigger(OnPointerEnter, EventTriggerType.PointerEnter);

        if (btn != null)
        {
            btn.onClick.AddListener(delegate { HandleMainMenuClicks(obj); });
        }

        return obj;
    }

    // https://answers.unity.com/questions/781726/how-do-i-add-a-listener-to-onpointerenter-ugui.html
    /// <summary>
    /// Custom function to add event triggers to an object.
    /// </summary>
    /// <param name="action">The action to perform</param>
    /// <param name="triggerType">The trigger type</param>
    private void AddEventTrigger(UnityAction action, EventTriggerType triggerType)
    {
        // Create a new TriggerEvent and add a listener
        EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
        trigger.AddListener((eventData) => action()); // you can capture and pass the event data to the listener

        // Create and initialise EventTrigger.Entry using the created TriggerEvent
        EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };

        // Add the EventTrigger.Entry to delegates list on the EventTrigger
        eventTrigger.triggers.Add(entry);
    }

    /// <summary>
    /// Overriden OnPointerEnter function used to play a sound.
    /// </summary>
    private void OnPointerEnter()
    {
        menuSounds.HoverSoundPlay();
    }



    /// <summary>
    /// Handles all menu clicks for this menu.
    /// </summary>
    /// <param name="obj">The object that requires an interaction</param>
    private void HandleMainMenuClicks(GameObject obj)
    {
        //menuSounds.HoverSoundPlay();
        switch (obj.name)
        {
            case strNewGame:
                Debug.Log("GOT New Game!");
                break;
            case strLoadGame:
                Debug.Log("GOT Load!");
                break;
            case strOptionsMenu:
                Debug.Log("GOT Options!");
                //OptionsMenuLoad();
                break;
            case strQuitGame:
                Debug.Log("GOT Quit!");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
                break;
            default:
                break;
        }
    }

    public void ShowMenu(bool value)
    {
        // Cleaner to spaghetti than canvasPanel.MenuCanvas.SetActive(value)
        canvasPanel.ShowCanvas(value);
    }
}


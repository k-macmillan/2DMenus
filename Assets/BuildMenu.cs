using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuildMenu : MonoBehaviour {
    
    public GameObject menuButton;
    public GameObject panel;
    public GameObject canvas;
    public GameObject slider;
    public GameObject label;

    //private static MenuState menuState = MenuState.NONE;
    private List<GameObject> buttons = new List<GameObject>();

    private MenuSounds menuSounds;
    //private EventTrigger eventTrigger = null;
    private OptionsControllerAudio optionsController;
    private BuildMainMenu buildMainMenu;

    private const float btnBuffer = 0.60f;
    private const float sliderBuffer = 0.50f;
    private const float lblBuffer = 0.50f;
    private const float lblHeight = 40.0f;
    private const int mainMenuButtonCount = 4;
    private const int optionsMenuButtonCount = 4;


    // Options Menu
    private const string strMasterVolume = "Master Volume";
    private const string strSFXVolume = "Sound Effects Volume";
    private const string strMusicVolume = "Music Volume";

    // Common Menu
    private const string strBack = "Back";

    public static MenuState MenuState { get; set; } = MenuState.NONE;

    public enum MenuType
    {
        MAIN,
        OPTIONS,
        SAVE,
        LOAD,
    }

    private void Awake()
    {
        menuSounds = new MenuSounds();        
        LoadMenu();        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    
    private void LoadMenu()
    {
        buttons.Clear();

        switch (MenuState)
        {
            case MenuState.NONE:
                buildMainMenu = new BuildMainMenu(menuSounds);
                break;
            case MenuState.MAINMENU:
                //MainMenuLoad();
                break;
            case MenuState.OPTIONSMENU:
                //OptionsMenuLoad();
                break;
            default:
                break;
        }
        
    }







    /// <summary>
    /// Handles all menu clicks for this menu.
    /// </summary>
    /// <param name="obj">The object that requires an interaction</param>
    private void HandleOptionsMenuClicks(GameObject obj)
    {
        switch (obj.name)
        {
            case strMasterVolume:
                Debug.Log("GOT Master Volume!");
                break;
            case strSFXVolume:
                Debug.Log("GOT SFX Volume!");
                break;
            case strMusicVolume:
                //OptionsMenuLoad();
                Debug.Log("GOT Music Volume!");
                break;
            case strBack:
                Debug.Log("GOT Back!");
                //MainMenuLoad();
                break;
            default:
                break;
        }
    }


    ///// <summary>
    ///// Loads the Options Menu and associated buttons.
    ///// </summary>
    //public void OptionsMenuLoad()
    //{
    //    LoadCanvasPanel();
    //    optionsController = new OptionsControllerAudio(menuSounds.MusicMixer);

    //    RectTransform panelRect = panelMain.GetComponent<RectTransform>();
    //    RectTransform lblRect = label.GetComponent<RectTransform>();
    //    RectTransform sliderRect = slider.GetComponent<RectTransform>();
    //    RectTransform btnRect = menuButton.GetComponent<RectTransform>();

    //    float diffWidth = Screen.width / 2.0f - sliderRect.rect.width * btnBuffer;
    //    float diffHeight = Screen.height / 2.0f - (sliderRect.rect.height * sliderBuffer + lblHeight * lblBuffer) * (optionsMenuButtonCount - 1) - btnRect.rect.height * btnBuffer;
        
    //    panelRect.offsetMin = new Vector2(diffWidth, diffHeight);
    //    panelRect.offsetMax = new Vector2(-diffWidth, -diffHeight);

    //    float sliderOffset = (optionsMenuButtonCount - 1) * (sliderRect.rect.height + lblHeight);
    //    float btnOffset = btnRect.rect.height * btnBuffer;

    //    Vector3 btnPosition = new Vector3
    //    {
    //        x = panelMain.transform.position.x,
    //        y = panelMain.transform.position.y + (sliderOffset + btnOffset) / 2.0f,
    //        z = panelMain.transform.position.z
    //    };

    //    // Offset function y movement
    //    btnPosition.y += (lblHeight + sliderRect.rect.height) / 2.0f;

    //    GameObject masterVol = InstantiateSlider(slider, strMasterVolume, ref btnPosition, MenuType.OPTIONS, panelMain);
    //    GameObject sfxVol = InstantiateSlider(slider, strSFXVolume, ref btnPosition, MenuType.OPTIONS, panelMain);
    //    GameObject musicVol = InstantiateSlider(slider, strMusicVolume, ref btnPosition, MenuType.OPTIONS, panelMain);
    //    btnPosition.y += sliderRect.rect.height / 2.0f;
    //    InstantiateButton(menuButton, strBack, ref btnPosition, MenuType.OPTIONS, panelMain);
    //    MenuState = MenuState.OPTIONSMENU;
        
    //}


    ///// <summary>
    ///// Used for instantiating and offsetting an Object.
    ///// </summary>
    ///// <param name="Obj">GameObject to be cloned</param>
    ///// <param name="DisplayText">Display text</param>
    ///// <param name="vec">Reference to offset vector</param>
    ///// <param name="Parent">The parent of the created GameObject</param>
    ///// <returns>The created GameObject</returns>
    //public GameObject InstantiateButton(GameObject Obj, string DisplayText, ref Vector3 Vec, MenuType MenuType, GameObject Parent = null)
    //{
    //    GameObject obj = Instantiate(Obj) as GameObject;
    //    obj.GetComponentInChildren<Text>().text = DisplayText;
    //    obj.name = DisplayText;
    //    obj.transform.SetParent(Parent.transform);
    //    Vec.y -= obj.GetComponent<RectTransform>().rect.height;
    //    obj.transform.position = new Vector3(Vec.x, Vec.y, Vec.z);

    //    Button btn = obj.GetComponent<Button>();
    //    eventTrigger = obj.AddComponent<EventTrigger>();
    //    AddEventTrigger(OnPointerEnter, EventTriggerType.PointerEnter);

    //    if (btn != null)
    //    {
    //        if (MenuType == MenuType.MAIN)
    //        {
    //            btn.onClick.AddListener(delegate { HandleMainMenuClicks(obj); });
    //        }
    //        else if (MenuType == MenuType.OPTIONS)
    //        {
    //            btn.onClick.AddListener(delegate { HandleOptionsMenuClicks(obj); });                
    //        }
    //    }

    //    return obj;
    //}


    /// <summary>
    /// Used for instantiating and offsetting an Object.
    /// </summary>
    /// <param name="Obj">GameObject to be cloned</param>
    /// <param name="DisplayText">Display text</param>
    /// <param name="vec">Reference to offset vector</param>
    /// <param name="Parent">The parent of the created GameObject</param>
    /// <returns>The created GameObject</returns>
    private GameObject InstantiateSlider(GameObject Obj, string DisplayText, ref Vector3 Vec, MenuType MenuType, GameObject Parent = null)
    {
        GameObject lbl = Instantiate(label) as GameObject;
        lbl.name = "Label " + DisplayText;
        Text txt = lbl.GetComponent<Text>();
        txt.text = DisplayText;

        lbl.transform.SetParent(Parent.transform);
        Vec.y -= lblHeight;
        lbl.transform.position = new Vector3(Vec.x, Vec.y, Vec.z);


        GameObject obj = Instantiate(Obj) as GameObject;
        obj.name = DisplayText;
        obj.transform.SetParent(Parent.transform);
        Vec.y -= obj.GetComponent<RectTransform>().rect.height;
        obj.transform.position = new Vector3(Vec.x - obj.GetComponent<RectTransform>().rect.width / 2.0f, Vec.y, Vec.z);

        Slider objSlider = obj.GetComponentInChildren<Slider>();
        switch (DisplayText)
        {
            case strMasterVolume:
                objSlider.value = optionsController.MasterVol;
                break;
            case strSFXVolume:
                objSlider.value = optionsController.SFXVol;
                break;
            case strMusicVolume:
                objSlider.value = optionsController.MusicVol;
                break;
            default:
                break;
        }
        objSlider.onValueChanged.AddListener(delegate { SliderValueChanged(objSlider); });

        //eventTrigger = obj.AddComponent<EventTrigger>();
        //AddEventTrigger(OnPointerEnter, EventTriggerType.);



        return obj;
    }


    //// https://answers.unity.com/questions/781726/how-do-i-add-a-listener-to-onpointerenter-ugui.html
    //private void AddEventTrigger(UnityAction action, EventTriggerType triggerType)
    //{
    //    // Create a new TriggerEvent and add a listener
    //    EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
    //    trigger.AddListener((eventData) => action()); // you can capture and pass the event data to the listener

    //    // Create and initialise EventTrigger.Entry using the created TriggerEvent
    //    EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };

    //    // Add the EventTrigger.Entry to delegates list on the EventTrigger
    //    eventTrigger.triggers.Add(entry);
    //}
    

    private void OnPointerEnter()
    {
        menuSounds.HoverSoundPlay();
    }
    


    private void SliderValueChanged(Slider objSlider)
    {
        switch (objSlider.name)
        {
            case strMasterVolume:
                optionsController.MasterVol = objSlider.value;
                break;
            case strSFXVolume:
                optionsController.SFXVol = objSlider.value;
                break;
            case strMusicVolume:
                optionsController.MusicVol = objSlider.value;
                break;
        }
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
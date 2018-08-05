using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuildOptionsMenu {

    // Helper classes
    private MenuSounds menuSounds;
    private CanvasPanel canvasPanel;

    // Constants

    // Prefabs
    private const string strLabelPrefab = "Prefabs/Label";
    private const string strSliderPrefab = "Prefabs/SFSlider";
    private const string strButtonPrefab = "Prefabs/SF Button";

    // Mixer
    private const string strMasterVolume = "Master Volume";
    private const string strSFXVolume = "Sound Effects Volume";
    private const string strMusicVolume = "Music Volume";

    private const string strBack = "Back";
    private const float btnBuffer = 0.60f;
    private const float sliderBuffer = 0.50f;
    private const float lblBuffer = 0.50f;
    private const float lblHeight = 40.0f;
    private const int optionsMenuButtonCount = 4;

    private EventTrigger eventTrigger = null;
    private OptionsControllerAudio optionsController;
    private BuildMainMenu buildMainMenu;

    public GameObject labelPrefab;
    public GameObject sliderPrefab;
    public GameObject buttonPrefab;

    public BuildOptionsMenu(MenuSounds MenuSounds, BuildMainMenu BuildMainMenu)
    {
        eventTrigger = null;
        buildMainMenu = BuildMainMenu;
        menuSounds = MenuSounds;
        menuSounds.StartMenuSounds();
        canvasPanel = new CanvasPanel();

        labelPrefab = Resources.Load<GameObject>(strLabelPrefab);
        sliderPrefab = Resources.Load<GameObject>(strSliderPrefab);
        buttonPrefab = Resources.Load<GameObject>(strButtonPrefab);

        OptionsMenuLoad();
    }


    /// <summary>
    /// Loads the Options Menu and associated buttons.
    /// </summary>
    public void OptionsMenuLoad()
    {
        optionsController = new OptionsControllerAudio(menuSounds.MusicMixer);

        RectTransform panelRect = canvasPanel.MenuPanel.GetComponent<RectTransform>();
        RectTransform lblRect = labelPrefab.GetComponent<RectTransform>();
        RectTransform sliderRect = sliderPrefab.GetComponent<RectTransform>();
        RectTransform btnRect = buttonPrefab.GetComponent<RectTransform>();

        float diffWidth = Screen.width / 2.0f - sliderRect.rect.width * btnBuffer;
        float diffHeight = Screen.height / 2.0f - (sliderRect.rect.height * sliderBuffer + lblHeight * lblBuffer) * (optionsMenuButtonCount - 1) - btnRect.rect.height * btnBuffer;

        panelRect.offsetMin = new Vector2(diffWidth, diffHeight);
        panelRect.offsetMax = new Vector2(-diffWidth, -diffHeight);

        float sliderOffset = (optionsMenuButtonCount - 1) * (sliderRect.rect.height + lblHeight);
        float btnOffset = btnRect.rect.height * btnBuffer;

        Vector3 btnPosition = new Vector3
        {
            x = canvasPanel.MenuPanel.transform.position.x,
            y = canvasPanel.MenuPanel.transform.position.y + (sliderOffset + btnOffset) / 2.0f,
            z = canvasPanel.MenuPanel.transform.position.z
        };

        // Offset function y movement
        btnPosition.y += (lblHeight + sliderRect.rect.height) / 2.0f;

        InstantiateSlider(sliderPrefab, strMasterVolume, ref btnPosition, canvasPanel.MenuPanel);
        InstantiateSlider(sliderPrefab, strSFXVolume, ref btnPosition, canvasPanel.MenuPanel);
        InstantiateSlider(sliderPrefab, strMusicVolume, ref btnPosition, canvasPanel.MenuPanel);
        btnPosition.y += sliderRect.rect.height / 2.0f;
        InstantiateButton(buttonPrefab, strBack, ref btnPosition, canvasPanel.MenuPanel);

    }


    /// <summary>
    /// Used for instantiating and offsetting an Object.
    /// </summary>
    /// <param name="Obj">GameObject to be cloned</param>
    /// <param name="DisplayText">Display text</param>
    /// <param name="vec">Reference to offset vector</param>
    /// <param name="Parent">The parent of the created GameObject</param>
    /// <returns>The created GameObject</returns>
    private GameObject InstantiateSlider(GameObject Obj, string DisplayText, ref Vector3 Vec, GameObject Parent = null)
    {
        GameObject lbl = Object.Instantiate(labelPrefab) as GameObject;
        lbl.name = "Label " + DisplayText;
        Text txt = lbl.GetComponent<Text>();
        txt.text = DisplayText;

        lbl.transform.SetParent(Parent.transform);
        Vec.y -= lblHeight;
        lbl.transform.position = new Vector3(Vec.x, Vec.y, Vec.z);


        GameObject obj = Object.Instantiate(Obj) as GameObject;
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
            btn.onClick.AddListener(HandleMainMenuClicks);
        }

        return obj;
    }

    private void HandleMainMenuClicks()
    {
        canvasPanel.ShowCanvas(false);
        buildMainMenu.ShowMenu(true);
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

    public void ShowMenu(bool value)
    {
        canvasPanel.ShowCanvas(value);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuildOptionsMenu : BaseMenu {

    // Helper classes
    private MenuSounds menuSounds;

    // Constants

    // Prefabs
    private const string strSliderPrefab = "Prefabs/SliderVolume";
    private const string strButtonPrefab = "Prefabs/SF Button";
    private const string strLabelName = "Label";

    // Mixer
    private const string strMasterVolume = "Master Volume";
    private const string strSFXVolume = "Sound Effects Volume";
    private const string strMusicVolume = "Music Volume";

    private const string strBack = "Back";
    private const float btnBuffer = 0.60f;
    private const float sliderBuffer = 0.50f;
    private const float lblBuffer = 0.50f;
    private const float lblHeight = 42.5f;
    private const int optionsMenuButtonCount = 4;

    private OptionsControllerAudio optionsController;
    private CanvasPanel parentMenu;

    private GameObject sliderPrefab;
    private GameObject buttonPrefab;


    public BuildOptionsMenu(MenuSounds MenuSounds, CanvasPanel ParentMenu)
    {
        eventTrigger = null;
        parentMenu = ParentMenu;
        menuSounds = MenuSounds;
        menuSounds.StartMenuSounds();
        canvasPanel = new CanvasPanel();

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
        RectTransform sliderRect = sliderPrefab.GetComponent<RectTransform>();
        RectTransform btnRect = buttonPrefab.GetComponent<RectTransform>();

        float diffWidth = Screen.width / 2.0f - sliderRect.rect.width * btnBuffer;
        float diffHeight = Screen.height / 2.0f - (sliderRect.rect.height * sliderBuffer + lblHeight * lblBuffer) * (optionsMenuButtonCount - 1) - btnRect.rect.height * btnBuffer;
        float sliderHeight = sliderRect.rect.height + lblHeight;

        panelRect.offsetMin = new Vector2(diffWidth, diffHeight);
        panelRect.offsetMax = new Vector2(-diffWidth, -diffHeight);

        float sliderOffset = (optionsMenuButtonCount - 1) * (sliderHeight);
        float btnOffset = btnRect.rect.height * btnBuffer;

        Vector3 btnPosition = new Vector3
        {
            x = canvasPanel.MenuPanel.transform.position.x,
            y = canvasPanel.MenuPanel.transform.position.y + (sliderOffset + btnOffset) / 2.0f,
            z = canvasPanel.MenuPanel.transform.position.z
        };

        // Offset function y movement
        btnPosition.y += sliderHeight / 2.0f;
        Vector3 offset = new Vector3(0.0f, -sliderHeight, 0.0f);

        InstantiateSlider(sliderPrefab, strMasterVolume, ref btnPosition, offset, canvasPanel.MenuPanel);
        InstantiateSlider(sliderPrefab, strSFXVolume, ref btnPosition, offset, canvasPanel.MenuPanel);
        InstantiateSlider(sliderPrefab, strMusicVolume, ref btnPosition, offset, canvasPanel.MenuPanel);
        offset.y = -(sliderRect.rect.height / 2.0f + btnRect.rect.height / 2.0f);
        InstantiateButton(buttonPrefab, strBack, canvasPanel.MenuPanel, ref btnPosition, offset);

    }


    /// <summary>
    /// Used for instantiating and offsetting a slider object.
    /// </summary>
    /// <param name="Obj">GameObject to be cloned</param>
    /// <param name="DisplayText">Display text</param>
    /// <param name="Position">Starting position</param>
    /// <param name="Offset">Offset from start to place this slider</param>
    /// <param name="Parent">The parent of the created GameObject</param>
    /// <returns>The created GameObject</returns>
    private GameObject InstantiateSlider(GameObject Obj, string DisplayText, ref Vector3 Position, Vector3 Offset = new Vector3(), GameObject Parent = null)
    {
        Transform lbl = Obj.GetComponentInChildren<Transform>().Find(strLabelName);
        if (lbl != null)
        {
            Text txt = lbl.GetComponentInChildren<Text>();
            txt.text = DisplayText;
        }

        GameObject obj = Object.Instantiate(Obj) as GameObject;
        obj.name = DisplayText;
        obj.transform.SetParent(Parent.transform);
        Position += Offset;
        obj.transform.position = new Vector3(Position.x - obj.GetComponent<RectTransform>().rect.width / 2.0f, Position.y, Position.z);

        SliderHandler(obj.GetComponentInChildren<Slider>());

        return obj;

    }


    /// <summary>
    /// Override of HandleMenuClicks to handle the back button
    /// </summary>
    /// <param name="obj"></param>
    protected override void HandleMenuClicks(GameObject obj)
    {
        // Only button is the back button...so...
        canvasPanel.ShowCanvas(false);
        parentMenu.ShowCanvas(true);
    }



    /// <summary>
    /// Overriden OnPointerEnter function used to play a sound.
    /// </summary>
    protected override void OnPointerEnter()
    {
        menuSounds.HoverSoundPlay();
    }


    private void SliderHandler(Slider objSlider)
    {
        switch (objSlider.name)
        {
            case strMasterVolume:
                objSlider.value = optionsController.MasterVol;
                objSlider.onValueChanged.AddListener(delegate { optionsController.MasterVol = objSlider.value; });
                break;
            case strSFXVolume:
                objSlider.value = optionsController.SFXVol;
                objSlider.onValueChanged.AddListener(delegate { optionsController.SFXVol = objSlider.value; });
                break;
            case strMusicVolume:
                objSlider.value = optionsController.MusicVol;
                objSlider.onValueChanged.AddListener(delegate { optionsController.MusicVol = objSlider.value; });
                break;
            default:
                break;
        }
    }
    
}

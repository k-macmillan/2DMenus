using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour {

    public GameObject menuButton;
    public GameObject panel;
    public GameObject canvas;

    private static string sceneName = "MainMenu";
    private List<GameObject> buttons = new List<GameObject>();    
    private GameObject canvasMain;
    private GameObject panelMain;

    private const float btnBuffer = 0.60f;
    private const int mainMenuButtonCount = 4;
    private const int optionsMenuButtonCount = 4;

    // Main Menu
    private const string NewGame = "New Game";
    private const string LoadGame = "Load";
    private const string OptionsMenu = "Options";
    private const string QuitGame = "Quit";

    // Options Menu
    private const string MasterVolume = "Master Volume";
    private const string SFXVolume = "Sound Effects Volume";
    private const string MusicVolume = "Music Volume";

    // Common Menu
    private const string Back = "Back";

    public enum MenuState
    {
        MAINMENU,
        PAUSEMENU,
        OPTIONSMENU,
        LOAD,
        SAVE,
    }

    public enum MenuType
    {
        MAIN,
        OPTIONS,
        SAVE,
        LOAD,
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            LoadMenu(MenuState.MAINMENU);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void LoadMenu(MenuState menuState)
    {
        buttons.Clear();

        switch (menuState)
        {
            case MenuState.MAINMENU:
                MainMenuLoad();
                break;
            case MenuState.OPTIONSMENU:
                OptionsMenuLoad();
                break;
            default:
                break;
        }
        
    }

    /// <summary>
    /// Cleans then loads a new canvas and panel for use with menus.
    /// </summary>
    public void LoadCanvasPanel()
    {
        Destroy(canvasMain);
        Destroy(panelMain);
        canvasMain = Instantiate(canvas);
        panelMain = Instantiate(panel);
        panelMain.transform.SetParent(canvasMain.transform);
    }


    /// <summary>
    /// Loads the Main Menu and associated buttons.
    /// </summary>
    public void MainMenuLoad()
    {
        LoadCanvasPanel();
        RectTransform panelRect = panelMain.GetComponent<RectTransform>();
        RectTransform btnRect = menuButton.GetComponent<RectTransform>();
        float diffWidth = Screen.width / 2.0f - btnRect.rect.width * btnBuffer;
        float diffHeight = Screen.height / 2.0f - btnRect.rect.height * btnBuffer * mainMenuButtonCount;

        panelRect.offsetMin = new Vector2(diffWidth, diffHeight);
        panelRect.offsetMax = new Vector2(-diffWidth, -diffHeight);

        Vector3 btnPosition = new Vector3
        {
            x = panelMain.transform.position.x,
            y = panelMain.transform.position.y + mainMenuButtonCount / 2.0f * btnRect.rect.height - btnRect.rect.height / 2.0f,
            z = panelMain.transform.position.z
        };

        // Offset function y movement
        btnPosition.y += menuButton.GetComponent<RectTransform>().rect.height;

        buttons.Clear();
        buttons.Add(InstantiateAdjustObj(menuButton, NewGame, ref btnPosition, MenuType.MAIN, panelMain));
        buttons.Add(InstantiateAdjustObj(menuButton, LoadGame, ref btnPosition, MenuType.MAIN, panelMain));
        buttons.Add(InstantiateAdjustObj(menuButton, OptionsMenu, ref btnPosition, MenuType.MAIN, panelMain));
        buttons.Add(InstantiateAdjustObj(menuButton, QuitGame, ref btnPosition, MenuType.MAIN, panelMain));
    }


    /// <summary>
    /// Handles all menu clicks for this menu.
    /// </summary>
    /// <param name="obj">The object that requires an interaction</param>
    private void HandleMainMenuClicks(GameObject obj)
    {
        switch (obj.name)
        {
            case "New Game":
                Debug.Log("GOT New Game!");
                break;
            case "Load":
                Debug.Log("GOT Load!");
                break;
            case "Options":
                Debug.Log("GOT Options!");
                OptionsMenuLoad();
                break;
            case "Quit":
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


    /// <summary>
    /// Handles all menu clicks for this menu.
    /// </summary>
    /// <param name="obj">The object that requires an interaction</param>
    private void HandleOptionsMenuClicks(GameObject obj)
    {
        switch (obj.name)
        {
            case "Master Volume":
                Debug.Log("GOT Master Volume!");
                break;
            case "SFX Volume":
                Debug.Log("GOT SFX Volume!");
                break;
            case "Music Volume":
                OptionsMenuLoad();
                Debug.Log("GOT Music Volume!");
                break;
            case "Back":
                Debug.Log("GOT Back!");
                MainMenuLoad();
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// Loads the Options Menu and associated buttons.
    /// </summary>
    public void OptionsMenuLoad()
    {
        LoadCanvasPanel();
        RectTransform panelRect = panelMain.GetComponent<RectTransform>();
        RectTransform btnRect = menuButton.GetComponent<RectTransform>();
        float diffWidth = Screen.width / 2.0f - btnRect.rect.width * btnBuffer;
        float diffHeight = Screen.height / 2.0f - btnRect.rect.height * btnBuffer * optionsMenuButtonCount;

        panelRect.offsetMin = new Vector2(diffWidth, diffHeight);
        panelRect.offsetMax = new Vector2(-diffWidth, -diffHeight);

        Vector3 btnPosition = new Vector3
        {
            x = panelMain.transform.position.x,
            y = panelMain.transform.position.y + optionsMenuButtonCount / 2.0f * btnRect.rect.height - btnRect.rect.height / 2.0f,
            z = panelMain.transform.position.z
        };

        // Offset function y movement
        btnPosition.y += menuButton.GetComponent<RectTransform>().rect.height;

        InstantiateAdjustObj(menuButton, MasterVolume, ref btnPosition, MenuType.OPTIONS, panelMain);
        InstantiateAdjustObj(menuButton, SFXVolume, ref btnPosition, MenuType.OPTIONS, panelMain);
        InstantiateAdjustObj(menuButton, MusicVolume, ref btnPosition, MenuType.OPTIONS, panelMain);
        InstantiateAdjustObj(menuButton, Back, ref btnPosition, MenuType.OPTIONS, panelMain);
    }


    /// <summary>
    /// Used for instantiating and offsetting an Object.
    /// </summary>
    /// <param name="Obj">GameObject to be cloned</param>
    /// <param name="DisplayText">Display text</param>
    /// <param name="vec">Reference to offset vector</param>
    /// <param name="Parent">The parent of the created GameObject</param>
    /// <returns>The created GameObject</returns>
    private GameObject InstantiateAdjustObj(GameObject Obj, string DisplayText, ref Vector3 vec, MenuType menuType, GameObject Parent = null)
    {
        GameObject obj = Instantiate(Obj) as GameObject;
        obj.GetComponentInChildren<Text>().text = DisplayText;
        obj.name = DisplayText;
        obj.transform.SetParent(Parent.transform);
        vec.y -= obj.GetComponent<RectTransform>().rect.height;
        obj.transform.position = vec;

        Button btn = obj.GetComponent<Button>();
        if (btn != null)
        {
            // TODO: These should be enums?
            if (menuType == MenuType.MAIN)
            {
                btn.onClick.AddListener(delegate { HandleMainMenuClicks(obj); });
            }
            else if (menuType == MenuType.OPTIONS)
            {
                btn.onClick.AddListener(delegate { HandleOptionsMenuClicks(obj); });                
            }
        }

        return obj;
    }
    

}
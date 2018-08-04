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
    private const float btnBuffer = 0.60f;
    private const int mainMenuButtonCount = 4;
    private GameObject canvasMain;
    private GameObject panelMain;

    private enum MenuEnums
    {
        MAINMENU,
        PAUSEMENU,
        OPTIONSMENU,
        LOAD,
        SAVE,
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            LoadMenu(MenuEnums.MAINMENU);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void LoadMenu(MenuEnums menuState)
    {
        buttons.Clear();

        switch (menuState)
        {
            case MenuEnums.MAINMENU:
                MainMenuLoad();
                break;
            case MenuEnums.OPTIONSMENU:
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
        panelMain.transform.parent = canvasMain.transform;
    }

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

        InstantiateAdjustObj(menuButton, "New Game", ref btnPosition, panelMain);
        InstantiateAdjustObj(menuButton, "Load", ref btnPosition, panelMain);
        InstantiateAdjustObj(menuButton, "Options", ref btnPosition, panelMain);
        InstantiateAdjustObj(menuButton, "Quit", ref btnPosition, panelMain);
    }
    

    private GameObject InstantiateAdjustObj(GameObject Obj, string Name, ref Vector3 vec, GameObject Parent = null)
    {
        GameObject obj = Instantiate(Obj) as GameObject;
        obj.GetComponentInChildren<Text>().text = Name;
        obj.transform.parent = Parent.transform;
        vec.y -= obj.GetComponent<RectTransform>().rect.height;
        obj.transform.position = vec;
        return obj;
    }


}
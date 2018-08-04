using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour {

    public GameObject menuButton;
    public GameObject panel;

    private static string sceneName = "MainMenu";
    private List<GameObject> buttons = new List<GameObject>();
    private const float btnBuffer = 0.60f;
    private const int mainMenuButtonCount = 4;

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

    public void MainMenuLoad()
    {
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        RectTransform btnRect = menuButton.GetComponent<RectTransform>();
        float diffWidth = Screen.width / 2.0f - btnRect.rect.width * btnBuffer;
        float diffHeight = Screen.height / 2.0f - btnRect.rect.height * btnBuffer * mainMenuButtonCount;

        panelRect.offsetMin = new Vector2(diffWidth, diffHeight);
        panelRect.offsetMax = new Vector2(-diffWidth, -diffHeight);

        Vector3 btnPosition = new Vector3
        {
            x = panel.transform.position.x,
            y = panel.transform.position.y + mainMenuButtonCount / 2.0f * btnRect.rect.height - btnRect.rect.height / 2.0f,
            z = panel.transform.position.z
        };

        // Offset function y movement
        btnPosition.y += menuButton.GetComponent<RectTransform>().rect.height;

        InstantiateAdjustObj(menuButton, "New Game", ref btnPosition, panel);
        InstantiateAdjustObj(menuButton, "Load", ref btnPosition, panel);
        InstantiateAdjustObj(menuButton, "Options", ref btnPosition, panel);
        InstantiateAdjustObj(menuButton, "Quit", ref btnPosition, panel);
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
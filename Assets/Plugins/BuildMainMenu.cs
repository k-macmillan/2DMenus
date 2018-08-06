using UnityEngine;
using UnityEngine.EventSystems;

public class BuildMainMenu : BaseMenu
{

    // Helper classes
    private MenuSounds menuSounds;
    //private CanvasPanel canvasPanel;


    // Const variables
    private const string strMainMenu = "MainMenu";
    private const string strNewGame = "New Game";
    private const string strLoadGame = "Load";
    private const string strOptionsMenu = "Options";
    private const string strQuitGame = "Quit";
    private const string strButtonPrefab = "Prefabs/SF Button";
    private const float btnBuffer = 0.60f;
    private const int mainMenuButtonCount = 4;

    private GameObject buttonPrefab;
    
    private BuildOptionsMenu buildOptionsMenu;
    private BuildSaveLoadMenu buildSaveLoadMenu;


    /// <summary>
    /// Constructor. Sets up the sound and canvas/panel. Loads button prefab and then loads the buttons.
    /// </summary>
    /// <param name="MenuSounds">Reference to the MenuSounds object</param>
    public BuildMainMenu(MenuSounds MenuSounds)
    {
        
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
        Vector3 offset = new Vector3(0.0f, -btnRect.rect.height, 0.0f);
        btnPosition -= offset;

        InstantiateButton(buttonPrefab, strNewGame, canvasPanel.MenuPanel, ref btnPosition, offset);        
        InstantiateButton(buttonPrefab, strLoadGame, canvasPanel.MenuPanel, ref btnPosition, offset);
        InstantiateButton(buttonPrefab, strOptionsMenu, canvasPanel.MenuPanel, ref btnPosition, offset);
        InstantiateButton(buttonPrefab, strQuitGame, canvasPanel.MenuPanel, ref btnPosition, offset);
    }




    /// <summary>
    /// Overriden OnPointerEnter function used to play a sound.
    /// </summary>
    protected override void OnPointerEnter()
    {
        menuSounds.HoverSoundPlay();
    }



    /// <summary>
    /// Handles all menu clicks for this menu.
    /// </summary>
    /// <param name="obj">The object that requires an interaction</param>
    protected override void HandleMenuClicks(GameObject obj)
    {
        EventSystem.current.SetSelectedGameObject(null);
        //menuSounds.HoverSoundPlay();
        switch (obj.name)
        {
            case strNewGame:
                Debug.Log("GOT New Game!");
                break;
            case strLoadGame:
                SetActive(false);
                if (buildSaveLoadMenu == null)
                {
                    buildSaveLoadMenu = new BuildSaveLoadMenu(menuSounds, canvasPanel);
                }
                else
                {
                    buildSaveLoadMenu.SetActive(true);
                }
                
                Debug.Log("GOT Load!");
                break;
            case strOptionsMenu:
                Debug.Log("GOT Options!");
                SetActive(false);
                if (buildOptionsMenu == null)
                {
                    buildOptionsMenu = new BuildOptionsMenu(menuSounds, canvasPanel);
                }
                else
                {                    
                    buildOptionsMenu.SetActive(true);
                }
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
        EventSystem.current.SetSelectedGameObject(null);
    }


}




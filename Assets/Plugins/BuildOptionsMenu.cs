using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildOptionsMenu {

    //private MenuSounds menuSounds;

    //public BuildOptionsMenu(MenuSounds MenuSounds)
    //{
    //    eventTrigger = null;
    //    menuSounds = MenuSounds;
    //    menuSounds.StartMenuSounds();
    //    canvasPanel = new CanvasPanel();

    //    buttonPrefab = Resources.Load<GameObject>(strButtonPrefab);
    //    MainMenuLoad();
    //}


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
}

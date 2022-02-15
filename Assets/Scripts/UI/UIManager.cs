using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    List<GameObject> UIObjects;

    [Header ("Accept choose UI objects")]
    public GameObject panel;
    [SerializeField] Text text;
    public Button buttonAgree;
    [SerializeField] Button buttonCancel;

    [Header("Settings panel UI objects")]
    [SerializeField] GameObject panelSettings;
    [SerializeField] Slider sliderSound;
    private bool isPanelSettingsActive = false;

    /*
    [Header("character portrait : warrior, archer, mage ")]
    [SerializeField] Sprite[] portraitObjects;
    [SerializeField] Image portraitImage;
    */
    
    private void Start()
    {
        instance = GetComponent<UIManager>();
        buttonAgree.onClick.AddListener(HidePanelAcceptChoose);
        buttonCancel.onClick.AddListener(HidePanelAcceptChoose);
        sliderSound.value = AudioListener.volume;

        // Adding toggling UI objects in list
        UIObjects.Add(panelSettings);
        UIObjects.Add(panel);
    }

    void ChangePortrait()
    {
        //portraitImage.sprite = portraitObjects[(int)PlayerManager.instance.className];
    }

    private void Update()
    {
        if (SceneHelper.instance.sceneIndex > 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) TogglePanelSettings(); // Settings
        }
    }

    // Settings panel
    private void TogglePanelSettings()
    {
        foreach (GameObject UIobj in UIObjects)
        {
            UIobj.SetActive(false);
        }

        if (isPanelSettingsActive) HidePanelSettings();
        else ShowPanelSettings();
    }

    public void ShowPanelSettings()
    {
        Time.timeScale = 0;
        CursorChangerScript.instance.isCursorAtcive = true;

        isPanelSettingsActive = true;
        panelSettings.SetActive(true);      
    }

    public void HidePanelSettings()
    {
        Time.timeScale = 1;
        CursorChangerScript.instance.isCursorAtcive = false;

        isPanelSettingsActive = false; 
        panelSettings.SetActive(false);    
    }

    public void SliderSoundOnChange()
    {
        AudioListener.volume = sliderSound.value;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        panelSettings.SetActive(false);
        if (SceneHelper.instance.sceneName != "MainMenuScene")
        {
            ShowPanelAcceptChoose(message: "Back to Main Menu?");
            buttonAgree.onClick.AddListener(BackToMainMenuAccept);
        }
    }

    void BackToMainMenuAccept()
    {
        SceneHelper.instance.LoadSceneByName("MainMenuScene");
    }


    // Accept panel
    public void ShowPanelAcceptChoose(string message)
    {
        Time.timeScale = 0;
        CursorChangerScript.instance.isCursorAtcive = true;

        text.text = message;
        panel.SetActive(true);
    }

    public void HidePanelAcceptChoose()
    {
        Time.timeScale = 1;
        CursorChangerScript.instance.isCursorAtcive = false;

        buttonAgree.onClick.RemoveAllListeners();
        buttonAgree.onClick.AddListener(SoundManager.instance.ButtonSoundPlay);
        buttonAgree.onClick.AddListener(HidePanelAcceptChoose);
        panel.SetActive(false);
    }

}

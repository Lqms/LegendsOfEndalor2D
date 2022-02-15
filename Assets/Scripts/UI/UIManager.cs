using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    List<GameObject> UIObjects = new List<GameObject>();

    [Header ("Accept choose UI objects")]
    public GameObject panelAcceptChoose;
    [SerializeField] Text textAcceptChoose;
    public Button buttonAcceptChooseAgree;
    [SerializeField] Button buttonAcceptChooseCancel;

    [Header("Settings panel UI objects")]
    [SerializeField] GameObject panelSettings;
    [SerializeField] Slider sliderSettingsSound;
    private bool isPanelSettingsActive = false;

    [Header("character portrait and info: warrior, archer, mage ")]
    [SerializeField] Sprite[] portraitObjects;
    [SerializeField] Image portraitImage;

    private void Start()
    {
        instance = GetComponent<UIManager>();

        buttonAcceptChooseAgree.onClick.AddListener(HidePanelAcceptChoose);
        buttonAcceptChooseCancel.onClick.AddListener(HidePanelAcceptChoose);

        if (panelSettings != null)
        {
            sliderSettingsSound.value = AudioListener.volume;
            UIObjects.Add(panelSettings);
        }

        if (panelAcceptChoose != null)
        {
            UIObjects.Add(panelAcceptChoose);
        }

        if (portraitImage != null)
        {
            Invoke("ChangePortrait", 0.1f);
        }
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
        AudioListener.volume = sliderSettingsSound.value;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        panelSettings.SetActive(false);
        if (SceneHelper.instance.sceneName != "MainMenuScene")
        {
            ShowPanelAcceptChoose(message: "Back to Main Menu?");
            buttonAcceptChooseAgree.onClick.AddListener(BackToMainMenuAccept);
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

        textAcceptChoose.text = message;
        panelAcceptChoose.SetActive(true);
    }

    public void HidePanelAcceptChoose()
    {
        Time.timeScale = 1;
        CursorChangerScript.instance.isCursorAtcive = false;

        buttonAcceptChooseAgree.onClick.RemoveAllListeners();
        buttonAcceptChooseAgree.onClick.AddListener(SoundManager.instance.ButtonSoundPlay);
        buttonAcceptChooseAgree.onClick.AddListener(HidePanelAcceptChoose);
        panelAcceptChoose.SetActive(false);
    }


    // Panel character info
    void ChangePortrait()
    {
        portraitImage.sprite = portraitObjects[(int)PlayerManager.instance.className];
    }
}

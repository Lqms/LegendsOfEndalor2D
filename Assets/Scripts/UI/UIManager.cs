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

    private void TogglePanelSettings()
    {
        foreach (GameObject UIobj in UIObjects)
        {
            UIobj.SetActive(false);
        }

        if (isPanelSettingsActive) HidePanelSettings();
        else ShowPanelSettings();
    }


    // Settings panel
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

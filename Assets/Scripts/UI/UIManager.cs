using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject[] UIObjects;

    [Header ("Accept panel")]
    public GameObject panelAccept;
    [SerializeField] Text textAccept;
    public Button buttonAgree;
    [SerializeField] Button buttonCancel;

    [Header("Settings panel")]
    [SerializeField] GameObject panelSettings;
    private bool isPanelSettingsActive = false;

    [Header("character portrait : warrior, archer, mage ")]
    [SerializeField] Sprite[] portraitObjects;
    [SerializeField] Image portraitImage;
    
    private void Start()
    {
        instance = GetComponent<UIManager>();
        buttonAgree.onClick.AddListener(HidePanelAccept);
        buttonCancel.onClick.AddListener(HidePanelAccept);

        Invoke("ChangePortrait", 1f);
    }

    void ChangePortrait()
    {
        portraitImage.sprite = portraitObjects[(int)PlayerManager.instance.className];
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
        isPanelSettingsActive = true;
        Time.timeScale = 0;
        panelSettings.SetActive(true);
        FindObjectOfType<CursorChangerScript>().isCursorAtcive = true;
    }

    public void HidePanelSettings()
    {
        isPanelSettingsActive = false;
        Time.timeScale = 1;
        panelSettings.SetActive(false);
        FindObjectOfType<CursorChangerScript>().isCursorAtcive = false;
    }


    // Accept panel
    public void ShowPanelAccept(string message)
    {
        Time.timeScale = 0;
        textAccept.text = message;
        FindObjectOfType<CursorChangerScript>().isCursorAtcive = true;
        panelAccept.SetActive(true);
    }

    public void HidePanelAccept()
    {
        Time.timeScale = 1;
        FindObjectOfType<CursorChangerScript>().isCursorAtcive = false;
        buttonAgree.onClick.RemoveAllListeners();
        buttonAgree.onClick.AddListener(SoundManager.instance.ButtonSoundPlay);
        buttonAgree.onClick.AddListener(HidePanelAccept);
        panelAccept.SetActive(false);
    }

}

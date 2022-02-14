using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header ("Accept panel")]
    public GameObject panelAccept;
    [SerializeField] Text textAccept;
    public Button buttonAgree;
    [SerializeField] Button buttonCancel;


    [Header("Settings panel")]
    [SerializeField] GameObject panelSettings;

    private void Start()
    {
        instance = GetComponent<UIManager>();
        buttonAgree.onClick.AddListener(HidePanelAccept);
        buttonCancel.onClick.AddListener(HidePanelAccept);
    }

    // Settings panel
    public void ShowPanelSettings()
    {
        Time.timeScale = 0;
        panelSettings.SetActive(true);
        FindObjectOfType<CursorChangerScript>().isCursorAtcive = true;
    }

    public void HidePanelSettings()
    {
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

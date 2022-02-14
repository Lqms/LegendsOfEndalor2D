using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSettingsManager : MonoBehaviour
{
    [SerializeField] GameObject panelSettings;
    [SerializeField] Slider sliderSound;

    private void Start()
    {
        sliderSound.value = AudioListener.volume;
        panelSettings.SetActive(false);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = sliderSound.value;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        panelSettings.SetActive(false);
        if (SceneHelper.instance.sceneName != "MainMenuScene")
        {
            UIManager.instance.ShowPanelAccept(message: "Back to Main Menu?");
            UIManager.instance.buttonAgree.onClick.AddListener(BackToMainMenuAccept);
        }
    }

    void BackToMainMenuAccept()
    {
        SceneHelper.instance.LoadSceneByName("MainMenuScene");
    }

}

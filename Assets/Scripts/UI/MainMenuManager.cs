using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Background image")]
    [SerializeField] Image imageBG;

    [Header("Text version")]
    [SerializeField] Text textVersion;


    // Buttons onclick
    public void Continue()
    {
        Debug.Log("Continue");
    }

    public void NewGame()
    {
        UIManager.instance.ShowPanelAcceptChoose(message: "Start a new game?");
        UIManager.instance.buttonAgree.onClick.AddListener(NewGameAccept);
    }

    void NewGameAccept()
    {
        SoundManager.instance.ButtonNewGameSoundPlay();
        imageBG.GetComponent<Animator>().SetTrigger("NewGame"); //Change scene on event in this anim
        textVersion.text = "";
        gameObject.SetActive(false);
    }

    public void LoadGame()
    {
        Debug.Log("There will be game loading...");
    }

    public void Settings()
    {
        UIManager.instance.ShowPanelSettings();
    }

    public void Exit()
    {
        UIManager.instance.ShowPanelAcceptChoose(message: "Exit game?");
        UIManager.instance.buttonAgree.onClick.AddListener(ExitAccept);
    }

    void ExitAccept()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}

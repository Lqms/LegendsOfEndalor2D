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
        UIManager.Instance.ShowPanelAcceptChoose(message: "Start a new game?");
        UIManager.Instance.ButtonAcceptChooseAgree.onClick.AddListener(NewGameAccept);
    }

    void NewGameAccept()
    {
        SoundManager.Instance.PlayNewGameSound();
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
        UIManager.Instance.ShowPanelSettings();
    }

    public void Exit()
    {
        UIManager.Instance.ShowPanelAcceptChoose(message: "Exit game?");
        UIManager.Instance.ButtonAcceptChooseAgree.onClick.AddListener(ExitAccept);
    }

    void ExitAccept()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}

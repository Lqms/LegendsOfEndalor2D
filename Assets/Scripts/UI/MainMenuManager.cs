using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Background image")]
    [SerializeField] private Image _imageBackground;

    [Header("Text version")]
    [SerializeField] private Text _textVersion;


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

    private void NewGameAccept()
    {
        SoundManager.Instance.PlayNewGameSound();
        _imageBackground.GetComponent<Animator>().SetTrigger("NewGame");
        _textVersion.text = "";
        gameObject.SetActive(false);
        Invoke("StartNewGame", 3f);
    }

    private void StartNewGame()
    {
        SceneHelper.Instance.LoadSceneByName(SceneNames.CreateCharacterScene);
    }

    public void LoadGame()
    {
        Debug.Log("There will be a game loading...");
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

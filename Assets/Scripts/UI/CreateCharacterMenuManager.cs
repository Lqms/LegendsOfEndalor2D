using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterMenuManager : MonoBehaviour
{

    [Header ("Background images")]
    [SerializeField] GameObject[] imagesBackground;

    [Header("Choose class UI Objects")]
    [SerializeField] GameObject panelChooseClass;
    [SerializeField] Text textChooseClassName;
    [SerializeField] Text textChooseClassInfo;

    private int index = 0;

    public void ButtonChooseClassNextOnClick()
    {
        index++;
        if (index >= imagesBackground.Length) index = 0;
    }

    public void AcceptButtonOnClick()
    {
        UIManager.instance.ShowPanelAcceptChoose(message: $"Start a game as {classNames[index]}");
        UIManager.instance.buttonAcceptChooseAgree.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        SceneHelper.instance.LoadSceneByName("TutorialScene");
    }
    
}

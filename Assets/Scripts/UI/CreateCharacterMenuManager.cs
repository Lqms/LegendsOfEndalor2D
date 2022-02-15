using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterMenuManager : MonoBehaviour
{
    [Header ("Background images")]
    [SerializeField] Sprite[] imagesBackground;

    [Header("Choose class UI Objects")]
    [SerializeField] GameObject panelChooseClass;
    [SerializeField] Text textChooseClassName;
    [SerializeField] Text textChooseClassInfo;

    private int index = 0;
    ClassOfPlayer className;

    public void ButtonChooseClassNextOnClick()
    {
        index++;
        if (index >= (int)ClassOfPlayer.COUNT) index = 0;

        switch (index)
        {
            case 0:
                className = ClassOfPlayer.Warrior;
                break;
            case 1:
                className = ClassOfPlayer.Archer;
                break;
            case 2:
                className = ClassOfPlayer.Mage;
                break;
        }

        panelChooseClass.GetComponent<Image>().sprite = imagesBackground[index];
        textChooseClassName.text = className.ToString();
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

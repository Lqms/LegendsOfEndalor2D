using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterMenuManager : MonoBehaviour
{
    [Header ("Background images and animators")]
    [SerializeField] Sprite[] imagesBackground;

    [Header("Choose class UI Objects")]
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

        GetComponent<Image>().sprite = imagesBackground[index];
        GetComponent<Animator>().SetTrigger(className.ToString());
        textChooseClassName.text = className.ToString();
        textChooseClassInfo.text = GetClassInfo(index);

    }

    private string GetClassInfo(int index)
    {
        TextAsset data = Resources.Load("Class info") as TextAsset;
        string[] info = data.ToString().Split('|');
        return info[index];
    }

    public void AcceptButtonOnClick()
    {
        UIManager.instance.ShowPanelAcceptChoose(message: $"Start a game as {className}");
        UIManager.instance.buttonAcceptChooseAgree.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        SceneHelper.instance.LoadSceneByName("TutorialScene");
    }
    
}

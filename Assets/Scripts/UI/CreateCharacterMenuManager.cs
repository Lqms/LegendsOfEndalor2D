using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterMenuManager : MonoBehaviour
{
    [Header ("Background and class images")]
    [SerializeField] Sprite[] imagesBackground;
    [SerializeField] Sprite[] imagesClass;

    [Header("Choose class UI Objects")]
    [SerializeField] Text textChooseClassName;
    [SerializeField] Text textChooseClassInfo;
    [SerializeField] Image imageClass;

    private int index = 0;
    ClassOfPlayer className;
    Color color;

    public void ButtonChooseClassNextOnClick()
    {
        index++;
        if (index >= (int)ClassOfPlayer.COUNT) index = 0;

        switch (index)
        {          
            case 0:
                className = ClassOfPlayer.Warrior;
                color = new Color(1, 0, 0);
                break;
            case 1:
                className = ClassOfPlayer.Archer;
                color = new Color(0, 1, 0);
                break;
            case 2:
                className = ClassOfPlayer.Mage;
                color = new Color(0, 0, 1);
                break;
        }

        GetComponent<Image>().sprite = imagesBackground[index];
        if (className == ClassOfPlayer.Warrior) GetComponent<Image>().transform.localScale = new Vector3(2, 2, 1);
        else GetComponent<Image>().transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Animator>().SetTrigger(className.ToString());

        imageClass.sprite = imagesClass[index];
        imageClass.GetComponent<Animator>().SetTrigger(className.ToString());

        textChooseClassName.text = className.ToString();
        textChooseClassName.color = color;
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

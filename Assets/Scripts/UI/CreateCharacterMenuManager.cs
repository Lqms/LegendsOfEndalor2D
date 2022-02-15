using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterMenuManager : MonoBehaviour
{
    ClassOfPlayer className;
    private int index = 0;
    
    [Header ("Background and class images")]
    [SerializeField] Sprite[] imagesBackground;
    [SerializeField] Sprite[] imagesClass;

    [Header("Choose class UI Objects")]
    [SerializeField] Text textChooseClassName;
    [SerializeField] Text textChooseClassInfo;
    [SerializeField] Image imageClass;
    Color textClassNameColor;
    float imageScaleX;
    float imageScaleY;

    public void ButtonChooseClassNextOnClick()
    {
        index++;
        if (index >= (int)ClassOfPlayer.COUNT) index = 0;

        switch (index)
        {          
            case 0:
                className = ClassOfPlayer.Warrior;
                textClassNameColor = new Color(1, 0, 0);
                imageScaleX = 1; // 1024
                imageScaleY = 1; // 512
                break;
            case 1:
                className = ClassOfPlayer.Archer;
                textClassNameColor = new Color(0, 1, 0);
                imageScaleX = 0.3f; //340
                imageScaleY = 0.6f; // 512
                break;
            case 2:
                className = ClassOfPlayer.Mage;
                textClassNameColor = new Color(0, 0, 1);
                imageScaleX = 0.3f; //340
                imageScaleY = 0.6f; // 512
                break;
        }

        GetComponent<Image>().sprite = imagesBackground[index];
        GetComponent<Animator>().SetTrigger(className.ToString());

        imageClass.sprite = imagesClass[index];
        imageClass.GetComponent<Animator>().SetTrigger(className.ToString());
        imageClass.transform.localScale = new Vector2(imageScaleX, imageScaleY);

        textChooseClassName.text = className.ToString();
        textChooseClassName.color = textClassNameColor;
        textChooseClassInfo.text = GetClassInfo(index);

    }

    private string GetClassInfo(int index)
    {
        TextAsset data = Resources.Load("Class info") as TextAsset;
        string[] info = data.ToString().Split('|');
        return info[index];
    }

    public void ChooseButtonOnClick()
    {
        UIManager.instance.ShowPanelAcceptChoose(message: $"Start a game as {className}");
        UIManager.instance.buttonAcceptChooseAgree.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        SceneHelper.instance.LoadSceneByName("TutorialScene");
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CreateCharacterMenuManager : MonoBehaviour
{
    public ClassOfPlayer _classOfPlayer;
    private int index = 0;
    
    [Header ("Background and class images")]
    [SerializeField] Sprite[] imagesBackground;
    [SerializeField] Sprite[] imagesCharacter;

    [Header("Choose class UI Objects")]
    [SerializeField] Text textChooseClassName;
    [SerializeField] Text textChooseClassInfo;
    [SerializeField] Image imageOfCharacter;
    private Color textClassNameColor;
    private float imageOfCharacterScaleX;
    private float imageOfCharacterScaleY;

    /// <summary>
    /// indexes: 0 - Warrior, 1 - Archer, 2 - Mage
    /// </summary>
    public void ButtonChooseClassNextOnClick()
    {
        index++;
        if (index >= 3) index = 0;

        switch (index)
        {          
            case 0:
                _classOfPlayer = ClassOfPlayer.Warrior;
                textClassNameColor = new Color(1, 0, 0);
                imageOfCharacterScaleX = 1; // 1024
                imageOfCharacterScaleY = 1; // 512
                break;
            case 1:
                _classOfPlayer = ClassOfPlayer.Archer;
                textClassNameColor = new Color(0, 1, 0);
                imageOfCharacterScaleX = 0.3f; //340
                imageOfCharacterScaleY = 0.6f; // 512
                break;
            case 2:
                _classOfPlayer = ClassOfPlayer.Mage;
                textClassNameColor = new Color(0, 0, 1);
                imageOfCharacterScaleX = 0.3f; //340
                imageOfCharacterScaleY = 0.6f; // 512
                break;
        }

        // BG image and anim
        gameObject.GetComponent<Image>().sprite = imagesBackground[index];
        gameObject.GetComponent<Animator>().SetTrigger(_classOfPlayer.ToString());

        // Character image, animn and scale
        imageOfCharacter.sprite = imagesCharacter[index];
        imageOfCharacter.GetComponent<Animator>().SetTrigger(_classOfPlayer.ToString());
        imageOfCharacter.transform.localScale = new Vector2(imageOfCharacterScaleX, imageOfCharacterScaleY);

        // texts
        textChooseClassName.text = _classOfPlayer.ToString();
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
        UIManager.instance.ShowPanelAcceptChoose(message: $"Start a game as {_classOfPlayer}");
        UIManager.instance.buttonAcceptChooseAgree.onClick.AddListener(StartGame);
        FileManager.instance.WriteToFile(filename: "Player class", text: _classOfPlayer.ToString());
    }

    public void StartGame()
    {
        SceneHelper.instance.LoadSceneByName("TutorialScene");
    }
    
}

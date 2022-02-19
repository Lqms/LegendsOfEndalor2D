using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateCharacterMenuManager : MonoBehaviour
{
    private ClassOfPlayer _classOfPlayer;
    private int _indexOfClass = 0;
    
    [Header ("Background and class sprites")]
    [SerializeField] private Sprite[] _spritesBackground;
    [SerializeField] private Sprite[] _spritesCharacter;

    [Header("Choose class UI Objects")]
    [SerializeField] private Text _textChooseClassName;
    [SerializeField] private Text _textChooseClassInfo;
    [SerializeField] private Image _imageOfCharacter;
    private Color _textClassNameColor;
    private float _imageOfCharacterScaleX;
    private float _imageOfCharacterScaleY;

    /// <summary>
    /// indexes: 0 - Warrior, 1 - Archer, 2 - Mage
    /// </summary>
    public void ButtonChooseClassNextOnClick()
    {
        _indexOfClass++;
        if (_indexOfClass >= 3) _indexOfClass = 0;

        switch (_indexOfClass)
        {          
            case 0:
                _classOfPlayer = ClassOfPlayer.Warrior;
                _textClassNameColor = new Color(1, 0, 0);
                _imageOfCharacterScaleX = 1; // 1024
                _imageOfCharacterScaleY = 1; // 512
                break;
            case 1:
                _classOfPlayer = ClassOfPlayer.Archer;
                _textClassNameColor = new Color(0, 1, 0);
                _imageOfCharacterScaleX = 0.3f; //340
                _imageOfCharacterScaleY = 0.6f; // 512
                break;
            case 2:
                _classOfPlayer = ClassOfPlayer.Mage;
                _textClassNameColor = new Color(0, 0, 1);
                _imageOfCharacterScaleX = 0.3f; //340
                _imageOfCharacterScaleY = 0.6f; // 512
                break;
        }

        // BG image and anim
        gameObject.GetComponent<Image>().sprite = _spritesBackground[_indexOfClass];
        gameObject.GetComponent<Animator>().SetTrigger(_classOfPlayer.ToString());

        // Character image, animn and scale
        _imageOfCharacter.sprite = _spritesCharacter[_indexOfClass];
        _imageOfCharacter.GetComponent<Animator>().SetTrigger(_classOfPlayer.ToString());
        _imageOfCharacter.transform.localScale = new Vector2(_imageOfCharacterScaleX, _imageOfCharacterScaleY);

        // texts
        _textChooseClassName.text = _classOfPlayer.ToString();
        _textChooseClassName.color = _textClassNameColor;
        _textChooseClassInfo.text = GetClassInfo(_indexOfClass);
    }

    private string GetClassInfo(int index)
    {
        TextAsset data = Resources.Load("Class info") as TextAsset;
        string[] info = data.ToString().Split('|');
        return info[index];
    }

    public void ChooseButtonOnClick()
    {
        UIManager.Instance.ShowPanelAcceptChoose(message: $"Start a game as {_classOfPlayer}");
        UIManager.Instance.ButtonAcceptChooseAgree.onClick.AddListener(StartGame);
        FileManager.instance.WriteToFile(filename: "Player class", text: _classOfPlayer.ToString());
    }

    public void StartGame()
    {
        SceneHelper.Instance.LoadSceneByName(SceneNames.TutorialScene);
    }
    
}

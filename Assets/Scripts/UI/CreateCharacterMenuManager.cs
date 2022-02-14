using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterMenuManager : MonoBehaviour
{
    [SerializeField] GameObject[] imagesBackground;
    string[] classNames = { "Warrior", "Archer", "Mage" };
    private int index = 0;
    
    public void NextClassButtonOnClick()
    {
        foreach (GameObject image in imagesBackground) image.SetActive(false);
        index++;
        if (index >= imagesBackground.Length) index = 0;
        imagesBackground[index].SetActive(true);
    }

    public void AcceptButtonOnClick()
    {
        UIManager.instance.ShowPanelAccept(message: $"Start a game as {classNames[index]}");
        UIManager.instance.buttonAgree.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        SceneHelper.instance.LoadSceneByName("TutorialScene");
    }
    
}

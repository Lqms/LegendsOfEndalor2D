using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerChooseClassManager : MonoBehaviour
{
    public ClassOfPlayer playerClass;
    public GameObject playerObject;

    [Header ("Warrior, Arhcer, Mage")]
    [SerializeField] GameObject[] characterPrefabs;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (SceneHelper.instance.sceneName == "MainMenuScene") Destroy(gameObject);
    }

    public void ChooseClassWarrior() {
        playerClass = ClassOfPlayer.Warrior;
        playerObject = characterPrefabs[(int)playerClass];
    }

    public void ChooseClassArcher()
    {
        playerClass = ClassOfPlayer.Archer;
        playerObject = characterPrefabs[(int)playerClass];
    }

    public void ChooseClassMage()
    {
        playerClass = ClassOfPlayer.Mage;
        playerObject = characterPrefabs[(int)playerClass];
    }

}

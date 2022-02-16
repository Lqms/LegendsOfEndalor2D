using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterCreatorScript : MonoBehaviour
{
    [Header("Character create: warrior, archer, mage")]
    [SerializeField] GameObject[] characterPrefabs;
    [SerializeField] ClassOfPlayer _classOfPlayer;
    [SerializeField] bool isTest = false;

    private string className;

    private void Start()
    {
        CreateCharacter();
    }

    private void CreateCharacter()
    {
        if (!isTest)
        {
            className = FileManager.instance.ReadFromFile(filename: "Player class");

            switch (className)
            {
                case "Warrior":
                    _classOfPlayer = ClassOfPlayer.Warrior;
                    break;
                case "Archer":
                    _classOfPlayer = ClassOfPlayer.Archer;
                    break;
                case "Mage":
                    _classOfPlayer = ClassOfPlayer.Mage;
                    break;
                default:
                    _classOfPlayer = ClassOfPlayer.Warrior;
                    break;
            }
        }


        GameObject playerObj = Instantiate(characterPrefabs[(int)_classOfPlayer]);
        playerObj.GetComponent<PlayerManager>()._classOfPlayer = _classOfPlayer;
        playerObj.GetComponent<PlayerManager>().spawnPoint = GameObject.Find("SpawnPoint");
        playerObj.GetComponent<PlayerManager>().Respawn();
        Destroy(gameObject);            
    }
}

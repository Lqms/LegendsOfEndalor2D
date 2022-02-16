using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterCreatorScript : MonoBehaviour
{
    [Header("Character create: warrior, archer, mage")]
    [SerializeField] GameObject[] characterPrefabs;
    private int index = 0;
    private ClassOfPlayer choosenClassName;

    private void Start()
    {
        CreateCharacter();
    }

    private void CreateCharacter()
    {
        string className = FileManager.instance.ReadFromFile(filename: "Player class");
        switch (className)
        {
            case "Warrior":
                index = 0;
                choosenClassName = ClassOfPlayer.Warrior;
                break;
            case "Archer":
                index = 1;
                choosenClassName = ClassOfPlayer.Archer;
                break;
            case "Mage":
                index = 2;
                choosenClassName = ClassOfPlayer.Mage;
                break;
            default:
                index = 0;
                choosenClassName = ClassOfPlayer.Warrior;
                break;
        }
        
        GameObject playerObj = Instantiate(characterPrefabs[index]);
        playerObj.GetComponent<PlayerManager>().className = choosenClassName;
        playerObj.GetComponent<PlayerManager>().spawnPoint = GameObject.Find("SpawnPoint");
        playerObj.GetComponent<PlayerManager>().Respawn();
        Destroy(gameObject);            
    }
}

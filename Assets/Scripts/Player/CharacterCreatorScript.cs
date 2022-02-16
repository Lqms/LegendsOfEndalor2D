using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterCreatorScript : MonoBehaviour
{
    [Header("Character create: warrior, archer, mage")]
    [SerializeField] GameObject[] characterPrefabs;

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
                Debug.Log("war");
                break;
            case "Archer":
                Debug.Log("arch");
                break;
            case "Mage":
                Debug.Log("mag");
                break;
            default:
                Debug.Log("Error");
                break;
        }

        /*
        GameObject playerObj = Instantiate(characterPrefabs[0]);
        playerObj.GetComponent<PlayerManager>().className = choosenClassName;
        playerObj.GetComponent<PlayerManager>().spawnPoint = GameObject.Find("SpawnPoint");
        playerObj.GetComponent<PlayerManager>().Respawn();
        Destroy(gameObject);      
        */
    }
}

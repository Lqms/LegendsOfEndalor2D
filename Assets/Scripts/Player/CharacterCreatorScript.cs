using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterCreatorScript : MonoBehaviour
{
    [Header("Character create: warrior, archer, mage")]
    [SerializeField] GameObject[] characterPrefabs;
    public ClassOfPlayer choosenClassName;

    private bool characterCreated = false;

    private void Awake() { DontDestroyOnLoad(gameObject); }

    private void Update() 
    {
        if (SceneHelper.instance.sceneIndex < 2) Destroy(gameObject);
        if (SceneHelper.instance.sceneIndex == 3) CreateCharacter();
    }

    private void CreateCharacter()
    {
        if (!characterCreated)
        {
            string className = File.ReadAllText("D:/My Folder/GitHubProjects/LegendsOfEndalor2Dv.0.1a/Assets/Resources/Player class.txt");
            Debug.Log($"Class of player: {className}");
            characterCreated = true;
            GameObject playerObj = Instantiate(characterPrefabs[(int)choosenClassName]);
            playerObj.GetComponent<PlayerManager>().className = choosenClassName;
            playerObj.GetComponent<PlayerManager>().spawnPoint = GameObject.Find("SpawnPoint");
            playerObj.GetComponent<PlayerManager>().Respawn();
            Destroy(gameObject);
        }
    }

}

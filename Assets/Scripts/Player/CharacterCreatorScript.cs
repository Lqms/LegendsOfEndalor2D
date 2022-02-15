using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreatorScript : MonoBehaviour
{
    [Header("Character create: warrior, archer, mage")]
    [SerializeField] GameObject[] characterPrefabs;
    public ClassOfPlayer choosenClassName;
    private bool characterCreated = false;

    private void Awake() {
        print("Awake");
        DontDestroyOnLoad(gameObject); 
    }

    private void Update() 
    {
        if (SceneHelper.instance.sceneIndex < 2)
        {
            print("<2");
            Destroy(gameObject);
        }
        if (SceneHelper.instance.sceneIndex == 3)
        {
            print("==3");
            CreateCharacter();
        }
    }

    private void CreateCharacter()
    {
        if (!characterCreated)
        {
            Debug.Log("Created");
            characterCreated = true;
            GameObject playerObj = Instantiate(characterPrefabs[(int)choosenClassName]);
            playerObj.GetComponent<PlayerManager>().className = choosenClassName;
            playerObj.GetComponent<PlayerManager>().Respawn();
            Destroy(gameObject, 1f);
        }
    }

}

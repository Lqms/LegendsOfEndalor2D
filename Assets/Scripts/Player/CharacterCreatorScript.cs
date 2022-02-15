using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (SceneHelper.instance.sceneIndex == 2) CreateCharacter();
    }

    private void CreateCharacter()
    {
        if (!characterCreated)
        {
            characterCreated = true;
            GameObject playerObj = Instantiate(characterPrefabs[(int)choosenClassName]);
            playerObj.GetComponent<PlayerManager>().className = choosenClassName;
            playerObj.GetComponent<PlayerManager>().Respawn();
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreatorScript : MonoBehaviour
{
    [Header("Character create: warrior, archer, mage")]
    [SerializeField] private GameObject[] _characterPrefabs;
    [SerializeField] private ClassOfPlayer _classOfPlayer;
    [SerializeField] private bool _isTest = false;

    private string _className;

    private void Start()
    {
        CreateCharacter();
    }

    private void CreateCharacter()
    {
        if (!_isTest)
        {
            _className = FileManager.instance.ReadFromFile(filename: "Player class");

            switch (_className)
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


        GameObject playerObj = Instantiate(_characterPrefabs[(int)_classOfPlayer]);
        playerObj.GetComponent<PlayerManager>()._classOfPlayer = _classOfPlayer;
        playerObj.GetComponent<PlayerManager>().spawnPoint = GameObject.Find("SpawnPoint");
        playerObj.GetComponent<PlayerManager>().Respawn();
        Destroy(gameObject);            
    }
}

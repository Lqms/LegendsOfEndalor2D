using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreatorScript : MonoBehaviour
{
    PlayerChooseClassManager playerChooseClassManager;

    [Header ("For tests")]
    [SerializeField] GameObject playerTestPrefab;



    void Start()
    {
        playerChooseClassManager = FindObjectOfType<PlayerChooseClassManager>();

        if (playerChooseClassManager != null)
        {
            GameObject player = Instantiate(playerChooseClassManager.playerObject, transform.position, Quaternion.identity);
            player.GetComponent<PlayerManager>().className = playerChooseClassManager.playerClass;
            Destroy(playerChooseClassManager.gameObject, 0.5f);
        }
        else Instantiate(playerTestPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject, 0.5f);
    }

}

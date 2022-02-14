using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreatorScript : MonoBehaviour
{
    PlayerChooseClassManager playerChooseClassManager;

    [Header ("For tests")]
    [SerializeField] GameObject playerTestPrefab;

    [Header("character portrait : warrior, archer, mage ")]
    [SerializeField] Sprite[] portraitObjects;
    [SerializeField] Image portraitImage;

    void Start()
    {
        playerChooseClassManager = FindObjectOfType<PlayerChooseClassManager>();

        if (playerChooseClassManager != null)
        {
            GameObject player = Instantiate(playerChooseClassManager.playerObject, transform.position, Quaternion.identity);
            player.GetComponent<PlayerManager>().className = playerChooseClassManager.playerClass;
            portraitImage.sprite = portraitObjects[(int)PlayerManager.instance.className];
            Destroy(playerChooseClassManager.gameObject, 0.5f);
        }
        else Instantiate(playerTestPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject, 0.5f);
    }

}

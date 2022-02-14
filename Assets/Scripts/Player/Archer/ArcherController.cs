using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    // Objects
    ArcherLegsController Legs;
    ArcherArmsController Arms;

    private void Start()
    {
        Legs = GetComponentInChildren<ArcherLegsController>();
        Arms = GetComponentInChildren<ArcherArmsController>();
    }

    private void Update()
    {
        if (FindObjectOfType<PlayerManager>() == null) return;
        if (PlayerManager.instance.onPause || Time.timeScale != 1) return;

        Legs.PlayerMovement();
        Legs.PlayerJump();
        Legs.PlayerDash();
        Arms.PlayerBlocking();
        Arms.PlayerAttack();
    }


    /// <summary>
    /// called in 'Shot' and 'Strike' animations
    /// </summary>
    public void StartShot()
    {
        FindObjectOfType<ArcherShotScript>().Shot();
    }
}

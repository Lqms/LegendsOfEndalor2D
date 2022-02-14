using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageController : MonoBehaviour
{
    // Objects
    MageLegsController Legs;
    MageArmsController Arms;

    private void Start()
    {
        Legs = GetComponentInChildren<MageLegsController>();
        Arms = GetComponentInChildren<MageArmsController>();
    }

    private void Update()
    {
        if (PlayerManager.instance.onPause || Time.timeScale != 1) return;

        if (!Arms.isBlocking)
        {
            Legs.PlayerMovement();
            Legs.PlayerJump();
            Legs.PlayerDash();
            Arms.PlayerAttack();
        }

        Arms.PlayerBlocking();
    }

    /// <summary>
    /// Called in 'Attack' and 'Strike' animations
    /// </summary>
    public void StartShot() 
    {
        FindObjectOfType<MageShotScript>().Shot();
    }

    /// <summary>
    /// called in 'Block' animation
    /// </summary>
    public void PlayMagicBarrierSound(AudioClip magicBarrierSound)
    {
        AudioSource audioSource = GetComponentInParent<AudioSource>();
        if (!audioSource.isPlaying && Arms.isBlocking)
        {
            audioSource.clip = magicBarrierSound;
            audioSource.Play();
        }

    }
}

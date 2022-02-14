using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClassOfPlayer
{
    Warrior,
    Archer,
    Mage,
    COUNT
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public ClassOfPlayer className;
    public bool onPause;

    GameObject spawnPoint;
    AudioSource audioSource;


    private void Start()
    {
        instance = GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    public void Respawn()
    {
        transform.position = spawnPoint.transform.position;
    }


    // Sounds. Called in Animations (except Jump)
    public void PlayActionSound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}

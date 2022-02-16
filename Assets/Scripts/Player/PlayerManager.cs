using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClassOfPlayer
{
    Warrior,
    Archer,
    Mage
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public ClassOfPlayer _classOfPlayer;
    public bool onPause;

    public GameObject spawnPoint;
    AudioSource audioSource;

    [Header("Stats")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] float maxMana = 100;
    [SerializeField] float maxEnergy = 100;

    [Header("Current stats")]
    public float currentHealth = 100;
    public float currentMana = 100;
    public float currentEnergy = 100;

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

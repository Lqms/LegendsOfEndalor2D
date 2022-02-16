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
    public float maxHealth = 100;
    public float maxMana = 100;
    public float maxEnergy = 100;

    [Header("Current stats")]
    public float currentHealth = 100;
    public float currentMana = 100;
    public float currentEnergy = 100;

    [Header("Regens")]
    public float regenHealth = 1;
    public float regenMana = 1;
    public float regenEnergy = 1;

    private void Start()
    {
        instance = GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    private void Update()
    {
        RegenPoints();
    }

    private void RegenPoints()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        else currentHealth += regenHealth * Time.deltaTime; 

        if (currentEnergy > maxEnergy) currentEnergy = maxEnergy;
        else currentEnergy += regenEnergy * Time.deltaTime; 

        if (currentMana > maxMana) currentMana = maxMana;
        else currentMana += regenMana * Time.deltaTime; 


        if (currentHealth < 0) currentHealth = 0;
        if (currentMana < 0) currentMana = 0;
        if (currentEnergy < 0) currentEnergy = 0;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource audioSrc;
    [SerializeField] AudioClip buttonClickSound;
    [SerializeField] AudioClip buttonNewGameSound;

    void Start()
    {
        instance = GetComponent<SoundManager>();
        audioSrc = gameObject.GetComponent<AudioSource>();

        Button[] buttons = FindObjectsOfType<Button>(includeInactive: true);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(ButtonSoundPlay);
        }
    }
    public void SoundPlayOneShot(AudioClip audioClip)
    {
        audioSrc.PlayOneShot(audioClip);
    }

    // UI sounds
    public void ButtonSoundPlay()
    {
        audioSrc.PlayOneShot(buttonClickSound);
    }

    public void ButtonNewGameSoundPlay()
    {
        audioSrc.PlayOneShot(buttonNewGameSound);
    }
}

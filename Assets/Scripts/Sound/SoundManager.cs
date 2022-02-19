using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource audioSrc;

    [Header ("UI sounds")]
    [SerializeField] private AudioClip _buttonClickSound;
    [SerializeField] private AudioClip _newGameSound;

    private void Start()
    {
        Instance = GetComponent<SoundManager>();
        audioSrc = gameObject.GetComponent<AudioSource>();

        Button[] buttons = FindObjectsOfType<Button>(includeInactive: true);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayButtonClickSound);
        }
    }
    public void SoundPlayOneShot(AudioClip audioClip)
    {
        audioSrc.PlayOneShot(audioClip);
    }

    // UI sounds
    public void PlayButtonClickSound()
    {
        audioSrc.PlayOneShot(_buttonClickSound);
    }

    public void PlayNewGameSound()
    {
        audioSrc.PlayOneShot(_newGameSound);
    }
}

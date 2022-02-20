using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPObject : MonoBehaviour
{
    public float HealthPoints = 100;
    [SerializeField] private AudioClip _hitSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void GetDamage(float damage)
    {
        HealthPoints -= damage;
        _audioSource.PlayOneShot(_hitSound);
        StartCoroutine(GetDamageAnimationCoroutine());
        if (HealthPoints <= 0) Destroy(gameObject);
    }

    IEnumerator GetDamageAnimationCoroutine()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.2f, 0.2f);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

}

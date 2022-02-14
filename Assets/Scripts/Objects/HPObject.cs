using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPObject : MonoBehaviour
{
    public float healthPoints = 100;
    [SerializeField] AudioClip hitSound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GetDamage(float damage)
    {
        healthPoints -= damage;
        audioSource.PlayOneShot(hitSound);
        StartCoroutine(GetDamageAnimationCoroutine());
        if (healthPoints <= 0) Destroy(gameObject);
    }

    IEnumerator GetDamageAnimationCoroutine()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.2f, 0.2f);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

}

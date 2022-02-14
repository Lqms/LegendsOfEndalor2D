using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpellBookScript : MonoBehaviour
{
    // Magic Barrier attributes
    [SerializeField] GameObject magicBarrierPrefab;
    [SerializeField] AudioClip magicBarrierSound;
    GameObject magicBarrierObj;
    bool isMagicBarrierSoundPlay = false;

    // Magic Barrier methods
    public void MagicBarrier(bool activate)
    {
        if (activate) CreateBarrier();
        else DestroyBarrier();
    }
    private void CreateBarrier()
    {
        magicBarrierObj = Instantiate(magicBarrierPrefab, transform.position, Quaternion.identity);
    }
    private void DestroyBarrier()
    {
        if (magicBarrierObj != null) Destroy(magicBarrierObj);
        gameObject.GetComponentInParent<AudioSource>().clip = null;
    }

}

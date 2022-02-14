using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    // Objects
    WarriorLegsController Legs;
    WarriorArmsController Arms;
    [SerializeField] GameObject DamageDealSpot;

    private void Start()
    {
        Legs = GetComponentInChildren<WarriorLegsController>();
        Arms = GetComponentInChildren<WarriorArmsController>();
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
    /// Called in 'Strike' animation
    /// </summary>
    public void StartShot()
    {
        FindObjectOfType<WarriorShotScript>().Shot();
    }

    /// <summary>
    /// Called in 'Attack' animation
    /// </summary>
    public void DealDamage()
    {
        StartCoroutine(ActivateDamageSpotCoroutine());
    }

    private IEnumerator ActivateDamageSpotCoroutine()
    {
        DamageDealSpot.SetActive(true);

        BoxCollider2D collider = DamageDealSpot.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2(0 + Legs.speedVector, 0.11f);
        collider.size = new Vector2(1.4f, 1.3f);
        collider.isTrigger = true;

        yield return new WaitForSeconds(0.1f);
        Destroy(collider);
        DamageDealSpot.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArmsController : MonoBehaviour
{
    // Objects
    ArcherLegsController LegsScript;

    // Components
    Animator animator;
    Rigidbody2D rb;
    
    // Flags
    public bool isBlocking = false;
    public bool isAltShot = false;

    // Sounds
    [SerializeField] AudioClip dodgeSound;

    private void Start()
    {
        LegsScript = FindObjectOfType<ArcherLegsController>();

        animator = gameObject.GetComponentInParent<Animator>();
        rb = gameObject.GetComponentInParent<Rigidbody2D>();     
    }

    // Combat Logic
    public void PlayerAttack()
    {
        if (LegsScript.isJump || LegsScript.isRun) return;

        if (Input.GetMouseButtonDown(0)) Attack(isAltAttack: false);
        if (Input.GetMouseButtonDown(1)) Attack(isAltAttack: true);

    }

    private void Attack(bool isAltAttack)
    {
        if (PlayerManager.instance.currentEnergy < 10) return;
        PlayerManager.instance.currentEnergy -= 10;

        isAltShot = isAltAttack;
        if (isAltAttack) animator.SetTrigger("Strike");
        else animator.SetTrigger("Attack");

    }

    // Combat Logic


    // Blocking Logic
    public void PlayerBlocking()
    {
        if (LegsScript.isJump || LegsScript.isRun) return;
        if (Input.GetKeyDown(KeyCode.Q)) Blocking();
    }

    private void Blocking()
    {
        if (PlayerManager.instance.currentEnergy < 10) return;
        PlayerManager.instance.currentEnergy -= 10;

        gameObject.GetComponentInParent<AudioSource>().PlayOneShot(dodgeSound);
        isBlocking = true;
        animator.SetTrigger("Dodge");
        StartCoroutine(ArcherDodgeCoroutine(jumpPower: LegsScript.jumpDistance));
    }

    private IEnumerator ArcherDodgeCoroutine(float jumpPower)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * LegsScript.speed * jumpPower, ForceMode2D.Impulse);
        rb.AddForce(Vector2.left * LegsScript.speed * jumpPower * LegsScript.speedVector * 2, ForceMode2D.Impulse);
        PlayerManager.instance.onPause = true;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f);
        PlayerManager.instance.onPause = false;
        isBlocking = false;
    }

    // Blocking Logic
}

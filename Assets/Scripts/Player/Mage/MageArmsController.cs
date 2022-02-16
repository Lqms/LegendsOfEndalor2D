using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageArmsController : MonoBehaviour
{
    // Objects
    MageLegsController LegsScript;
    MageSpellBookScript SpellBookScript;

    // Components
    Animator animator;

    // Flags
    public bool isBlocking = false;
    public bool isAltShot = false;

    private void Start()
    {
        LegsScript = FindObjectOfType<MageLegsController>();
        SpellBookScript = FindObjectOfType<MageSpellBookScript>();

        animator = gameObject.GetComponentInParent<Animator>();
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
        if (isAltShot) animator.SetTrigger("Strike");
        else animator.SetTrigger("Attack");
    }

    // Combat Logic


    // Blocking Logic
    public void PlayerBlocking()
    {
        if (LegsScript.isJump || LegsScript.isRun) return;
        if (Input.GetKeyDown(KeyCode.Q)) Blocking(state: true);
        if (Input.GetKeyUp(KeyCode.Q)) Blocking(state: false);
    }

    private void Blocking(bool state)
    {
        if (PlayerManager.instance.currentMana < 10) return; //!!! Coroutine
        PlayerManager.instance.currentMana -= 10;
        isBlocking = state;
        animator.SetBool("isBlocking", state);
        SpellBookScript.MagicBarrier(state);
    }

    // Blocking Logic
}

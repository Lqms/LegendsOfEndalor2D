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

        if (Input.GetMouseButton(0)) Attack(isAltAttack: false);
        if (Input.GetMouseButtonDown(1)) Attack(isAltAttack: true);

    }

    private void Attack(bool isAltAttack)
    {
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
        isBlocking = state;
        animator.SetBool("isBlocking", state);
        SpellBookScript.MagicBarrier(state);
    }

    // Blocking Logic
}

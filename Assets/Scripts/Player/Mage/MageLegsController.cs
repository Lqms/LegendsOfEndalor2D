using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageLegsController : MonoBehaviour
{
    // Components
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [Header("Jump")]
    [SerializeField] LayerMask maskGround;
    [SerializeField] AudioClip jumpSound;
    public float jumpDistance = 1.3f;
    public bool isJump = false;
    public bool isRun = false;
    float radiusLegs = 0.04f;

    [Header("Movement")]
    public float speed = 5f;
    public int speedVector = 1;

    [Header("Dash")]
    [SerializeField] AudioClip dashSound;


    private void Start()
    {
        animator = gameObject.GetComponentInParent<Animator>();
        spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    // Jump Logic
    public void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJump) Jump(distance: jumpDistance);
    }

    private void Jump(float distance)
    {
        if (PlayerManager.instance.currentEnergy < 10) return;
        PlayerManager.instance.currentEnergy -= 10;

        gameObject.GetComponentInParent<AudioSource>().PlayOneShot(jumpSound);
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * speed * distance, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        PlayerOnGround(Physics2D.OverlapCircle(transform.position, radiusLegs, maskGround));
    }

    private void PlayerOnGround(bool onGround)
    {
        animator.SetBool("isJumping", !onGround);
        isJump = !onGround;
    }

    // Jump Logic

    // Movement Logic
    public void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A)) Movement(toTheRight: false);
        if (Input.GetKey(KeyCode.D)) Movement(toTheRight: true);
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) PlayerStop();
    }

    private void Movement(bool toTheRight)
    {
        speedVector = toTheRight ? 1 : -1;
        spriteRenderer.flipX = !toTheRight;
        isRun = true;
        animator.SetBool("isRunning", true);
        float velocity = PlayerManager.instance.currentEnergy >= 25 ? speed : speed / 2;
        rb.velocity = new Vector2(velocity * speedVector, rb.velocity.y);
    }

    private void PlayerStop()
    {
        isRun = false;
        animator.SetBool("isRunning", false);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    // Movement Logic

    // Dash Logic
    public void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isJump) Dash();
    }

    private void Dash()
    {
        if (PlayerManager.instance.currentMana < 10) return;
        PlayerManager.instance.currentMana -= 10;

        animator.SetBool("isRunning", false);
        animator.SetTrigger("Dash");
        gameObject.GetComponentInParent<AudioSource>().PlayOneShot(dashSound);
        rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y);
        StartCoroutine(MageDashCoroutine(dashPower: 500));
    }

    private IEnumerator MageDashCoroutine(float dashPower)
    {
        PlayerManager.instance.onPause = true;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(0, 0, 0, 0);
        rb.AddForce(Vector2.right * speedVector * speed * dashPower, ForceMode2D.Force);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
        PlayerManager.instance.onPause = false;
        PlayerStop();
    }
    // Dash Logic
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherLegsController : MonoBehaviour
{
    // Components
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [Header ("Jump")]
    [SerializeField] LayerMask maskGround;
    [SerializeField] AudioClip jumpSound;
    public float jumpDistance = 1.3f;
    public bool isJump = false;
    public bool isRun = false;
    float radiusLegs = 0.04f;

    [Header ("Movement")]
    public float speed = 5f;
    public int speedVector = 1;

    [Header ("Dash")]
    [SerializeField] GameObject dashWindObj;
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
        speedVector = toTheRight? 1: -1;     
        spriteRenderer.flipX = !toTheRight;
        isRun = true;
        animator.SetBool("isRunning", true);
        rb.velocity = new Vector2(speed * speedVector, rb.velocity.y);
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
        animator.SetBool("isRunning", false);
        animator.SetTrigger("Dash");
        gameObject.GetComponentInParent<AudioSource>().PlayOneShot(dashSound);
        rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y);
        CreateDashWind();
        StartCoroutine(ArcherDashCoroutine(dashPower: 100));
    }

    private void CreateDashWind()
    {
        float x = transform.position.x - 1 * speedVector;
        float y = transform.position.y + 0.3f;
        Vector2 spot = new Vector2(x, y);
        GameObject dashWind = Instantiate(dashWindObj, spot, Quaternion.identity);
        dashWind.GetComponent<SpriteRenderer>().flipX = speedVector < 0;
    }

    private IEnumerator ArcherDashCoroutine(float dashPower)
    {
        PlayerManager.instance.onPause = true;
        rb.AddForce(Vector2.right * speedVector * speed * dashPower, ForceMode2D.Force);
        yield return new WaitForSeconds(0.7f);
        PlayerManager.instance.onPause = false;
        PlayerStop();
    }
    // Dash Logic
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorProjectileScript : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;
    public float damage;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    public void SetDirection(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = direction.normalized * speed;
    }

    /// <summary>
    /// Hiding not-trigger projectile
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("RigidObject"))
        {
            if (!gameObject.GetComponent<CapsuleCollider2D>().isTrigger)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

            }
        }
    }

}

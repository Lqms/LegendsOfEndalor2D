using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherProjectileScript : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;
    public float damage;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("RigidObject"))
        {          
            HPObject object_hp = collision.gameObject.GetComponent<HPObject>();
            if (object_hp != null) ObjectDamage(object_hp);

            rb.velocity = Vector2.zero;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// hiding not-trigger projectiles
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("RigidObject"))
        {
            if (!gameObject.GetComponent<BoxCollider2D>().isTrigger)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            }
        }
    }

    private void ObjectDamage(HPObject object_hp)
    {
        // Pushback
        int speedVector = FindObjectOfType<ArcherLegsController>().speedVector;
        object_hp.GetComponent<Rigidbody2D>().AddForce(Vector2.up * damage, ForceMode2D.Impulse);
        object_hp.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speedVector * damage, ForceMode2D.Impulse);
        
        object_hp.GetDamage(damage);
    }

    public void SetDirection(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = direction.normalized * speed;
    }
}

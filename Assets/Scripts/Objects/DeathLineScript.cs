using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLineScript : MonoBehaviour
{
    [SerializeField] float colliderPosY;

    private void Start()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(500, 10);
        collider.offset = new Vector2(0, colliderPosY);
        collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerManager>() != null)
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            player.Respawn();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDamageDealSpotScript : MonoBehaviour
{
    public float damage = 24;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("RigidObject"))
        {
            HPObject object_hp = collision.gameObject.GetComponent<HPObject>();
            if (object_hp != null) ObjectDamage(object_hp);
        }
    }

    private void ObjectDamage(HPObject object_hp)
    {
        // Pushback
        int speedVector = FindObjectOfType<WarriorLegsController>().speedVector;
        object_hp.GetComponent<Rigidbody2D>().AddForce(Vector2.up * damage, ForceMode2D.Impulse);
        object_hp.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speedVector * damage, ForceMode2D.Impulse);
        
        object_hp.GetDamage(damage);
    }
}

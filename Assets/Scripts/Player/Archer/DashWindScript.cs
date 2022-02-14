using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWindScript : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Dash");
        Destroy(gameObject, 0.4f);
    }
}

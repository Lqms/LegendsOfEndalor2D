using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollowPlayer : MonoBehaviour
{
    [SerializeField] PlayerManager target; 
    Vector3 offset; 
    Camera cam;
    float zoomValue;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        Invoke("FindPlayer", 0.5f);
    }

    void Update()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            print("Zoomed!");
            zoomValue = Input.GetAxis("Mouse ScrollWheel");
            if (zoomValue > 0 && cam.orthographicSize > 2) cam.orthographicSize -= 1;
            else if (zoomValue < 0 && cam.orthographicSize < 4) cam.orthographicSize += 1;
        }

        if (target != null) transform.position = target.transform.position + offset;
    }
    
    void FindPlayer()
    {
        target = FindObjectOfType<PlayerManager>();
        offset = transform.position - target.transform.position;
    }
}

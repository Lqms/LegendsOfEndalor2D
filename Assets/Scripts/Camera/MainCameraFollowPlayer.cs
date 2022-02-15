using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollowPlayer : MonoBehaviour
{
    [Header("Camera settings")]
    [SerializeField] float maxZoomValue = 4;
    [SerializeField] float minZoomValue = 2;
    [SerializeField] PlayerManager target;
    private Camera mainCamera;
    private Vector3 offset;
    private float zoomValue;

    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        Invoke("FindPlayer", 0.5f);
    }

    void Update()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            print("Zoomed!");
            zoomValue = Input.GetAxis("Mouse ScrollWheel");
            if (zoomValue > 0 && mainCamera.orthographicSize > minZoomValue) mainCamera.orthographicSize -= 1;
            else if (zoomValue < 0 && mainCamera.orthographicSize < maxZoomValue) mainCamera.orthographicSize += 1;
        }

        if (target != null) transform.position = target.transform.position + offset;
    }
    
    void FindPlayer()
    {
        target = FindObjectOfType<PlayerManager>();
        offset = transform.position - target.transform.position;
    }
}

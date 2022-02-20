using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollowPlayer : MonoBehaviour
{
    [Header("Camera settings")]
    [SerializeField] private float _maxZoomValue = 4;
    [SerializeField] private float _minZoomValue = 2;

    private PlayerManager _target;
    private Camera _mainCamera;
    private Vector3 _offset;
    private float _currentZoomValue;

    void Start()
    {
        _mainCamera = gameObject.GetComponent<Camera>();
        Invoke("FindPlayer", 0.1f);
    }

    void Update()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            _currentZoomValue = Input.GetAxis("Mouse ScrollWheel");
            if (_currentZoomValue > 0 && _mainCamera.orthographicSize > _minZoomValue) _mainCamera.orthographicSize -= 1;
            else if (_currentZoomValue < 0 && _mainCamera.orthographicSize < _maxZoomValue) _mainCamera.orthographicSize += 1;
        }

        if (_target != null) transform.position = _target.transform.position + _offset;
    }
    
    void FindPlayer()
    {
        _target = FindObjectOfType<PlayerManager>();
        _offset = transform.position - _target.transform.position;
    }
}

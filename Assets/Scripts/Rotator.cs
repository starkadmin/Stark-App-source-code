using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector3 _originMousePosition;
    private bool _isDragging;
    private float _rotateSensitivity = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _originMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            if (Input.mousePosition != _originMousePosition)
            {
                float movement =  _originMousePosition.x - Input.mousePosition.x * _rotateSensitivity;
                Debug.Log(movement);
                _originMousePosition = Input.mousePosition;

                transform.Rotate(transform.up, movement);
            }
        }
    }
}

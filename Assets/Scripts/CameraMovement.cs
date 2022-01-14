using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float _speed = 1;
    public Vector3 _lookAtPoint;

    void Update()
    {
        float h = -Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.RotateAround(_lookAtPoint, Vector3.up, h * _speed);
        transform.LookAt(_lookAtPoint);
    }
}

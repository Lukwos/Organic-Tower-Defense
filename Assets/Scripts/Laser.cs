using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer _line;
    public float _duration = 1;

    public Vector3 _start;
    public Vector3 _end;

    void Start()
    {
        _line.SetPosition(0, _start);
        _line.SetPosition(1, _end);
        Destroy(gameObject, _duration);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Range : MonoBehaviour
{
    public Material _material;

    public void Set(Turret turret)
    {
        if (turret)
        {
            transform.position = turret.transform.position + Vector3.up * 50;
            _material.SetFloat("_Radius", turret._range / 100);
        }
        gameObject.SetActive(turret);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excavator : MonoBehaviour
{
    public int _ironQuantity;
    public int _goldQuantity;

    public void Excavate()
    {
        ResourcesManager._current.MoveResources(_ironQuantity, _goldQuantity);
    }

    public void OnKilled() { Destroy(gameObject); }
}

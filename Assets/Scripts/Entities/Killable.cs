using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public int _health = 100;

    public void TakeDamage(int firepower)
    {
        _health -= firepower;

        OnDamageTaken();

        if (_health <= 0)
        {
            OnDied();
        }
    }

    protected virtual void OnDied()
    {
        Destroy(gameObject);
    }

    protected virtual void OnDamageTaken() { }
}

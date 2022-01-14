using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Killable
{
    public int _firepower = 100;
    public int _rewardMoney = 100;
    public NavMeshAgent _navMeshAgent;

    Unit _target;

    void Update()
    {
        _target = FindTarget();
        if (_target)
        {
            _navMeshAgent.SetDestination(_target.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_target && collision.gameObject == _target.gameObject)
        {
            _target.TakeDamage(_firepower);
            Destroy(gameObject);
        }
    }

    public void TakeReward() { ResourcesManager._current.MoveResources(_rewardMoney, 0); }

    Unit FindTarget()
    {
        float closestDistance = float.PositiveInfinity;
        Unit closestUnit = null;
        foreach (var unit in FindObjectsOfType<Unit>())
        {
            float distance = Vector3.Distance(transform.position, unit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestUnit = unit.GetComponent<Unit>();
            }
        }
        return closestUnit;
    }

    override protected void OnDied()
    {
        base.OnDied();
        TakeReward();
    }
}

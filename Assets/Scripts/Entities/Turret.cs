using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Enemy _target;
    public float _range = 10;
    public float _sightAngle = 1;
    public int _firepower = 100;
    public float _cooldown = 1;
    public float _speed = 1;

    public Transform _turret;

    public GameObject _laserPrefab;

    bool _canShoot = true;
    bool _isAiming = false;

    void Update()
    {
        if (!_target)
        {
            _target = FindTarget();
        }
        if (_target)
        {
            Aim();
            if (_isAiming && _canShoot)
            {
                Shoot();
            }
        }
    }

    IEnumerator CoolDownCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_cooldown);
        _canShoot = true;
    }

    Enemy FindTarget()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < _range)
            {
                return enemy;
            }
        }
        return null;
    }

    void Aim()
    {
        float angle = Vector3.SignedAngle(_turret.forward, _target.transform.position - transform.position, transform.up);
        float clampAngle = Mathf.Clamp(angle, -_speed * Time.deltaTime, _speed * Time.deltaTime);
        _isAiming = Mathf.Abs(angle) < _sightAngle;
        _turret.Rotate(0, clampAngle, 0);
    }

    void Shoot()
    {
        //Debug.DrawLine(_turret.position, _target.transform.position, Color.red, 1);
        var laser = Instantiate(_laserPrefab).GetComponent<Laser>();
        laser._start = transform.position;
        laser._end = _target.transform.position;
        _target.TakeDamage(_firepower);
        StartCoroutine(CoolDownCoroutine());
    }

    public void OnKilled() { Destroy(gameObject); }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IPoolable
{
    [SerializeField] private GameObject _bloodSplash;

    private PoolManager _poolManager;
    
    private Vector3 _direction;
    private Transform _target;
    private float _speed = 0.5f;

    private int _damage;

    public void InitArrow(Transform target, int damage)
    {
        _poolManager = InjectBox.Get<PoolManager>();
        _target = target;
        _direction = _target.position - transform.position;
        _damage = damage;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_direction * _speed);
        CheckForHit();
    }

    private void CheckForHit()
    {
        if (Vector3.Distance(_target.position, transform.position) < 0.3f)
        {
            _target.GetComponent<Enemy>().GetDamage(_damage);
            _poolManager.GetOrCreate(_bloodSplash, null, transform.position);
            Release();
        }
    }

    private void Release()
    {
        _poolManager.Release(gameObject.name, gameObject);
    }

    public void OnActivate(object argument = default) { }
    public void OnDeactivate(object argument = default) { }
}

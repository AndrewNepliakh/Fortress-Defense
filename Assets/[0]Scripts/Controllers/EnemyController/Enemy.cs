using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    private static readonly int IsAtFortress = Animator.StringToHash("isAtFortress");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    
    private LevelManager _levelManager;
    private PoolManager _poolManager;
    private Animator _animator;
    
    [SerializeField] private int _hitPoints;
    [SerializeField] private int _damage;

    [SerializeField] private float _speed;

    private float _distanseToFortress = 3.0f;
    private Vector3 _direction;

    private int _currentHitpounts;

    private bool _isAtFortress;

    public void OnActivate(object argument = default)
    {
        _levelManager = InjectBox.Get<LevelManager>();
        _poolManager = InjectBox.Get<PoolManager>();
        _animator = GetComponent<Animator>();
        
        _currentHitpounts = _hitPoints;
        _direction = new Vector3(_speed, 0.0f, 0.0f);
    }

    private void FixedUpdate()
    {
        CheckForProximityFortress();
        if(!_isAtFortress) Move();
    }

    private void Move()
    {
        transform.Translate(_direction);
    }

    private void CheckForProximityFortress()
    {
        var position = transform.position;
        var fortressPosition = _levelManager.Fortress.transform.position;

        if (Vector3.Distance(position, fortressPosition) < _distanseToFortress && !_isAtFortress)
        {
            _animator.SetBool(IsAtFortress, true);
            _isAtFortress = true;
        }
    }

    public int GetDamage(int value)
    {
        _currentHitpounts -= value;
        if (_currentHitpounts < 0)
        {
            _animator.SetBool(IsDead, true);
            _levelManager.RemoveEnemy(this);
            EventManager.TriggerEvent<OnEnemyIsDeadEvent>();
        }

        return _currentHitpounts;
    }

    private void DoDamage()
    {
        EventManager.TriggerEvent(new OnEnemyDoDamageEvent {Damage = _damage});
    }

    public void Release()
    {
        _poolManager.Release(gameObject.name, gameObject);
    }
    

    public void OnDeactivate(object argument = default)
    {
    }
}
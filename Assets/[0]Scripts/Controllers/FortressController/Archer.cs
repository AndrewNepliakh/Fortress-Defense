using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    private static readonly int HasTarget = Animator.StringToHash("hasTarget");

    private int _damage = 5;
    
    private LevelManager _levelManager;
    private PoolManager _poolManager;
    private Animator _animator;

        [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _arrowPrefab;
    
    private List<Enemy> _enemies = new List<Enemy>();

    private Transform _target;
    private float _archerRange = 8.0f;

    [SerializeField] private float _delay;
    private float _previousTime = 0.0f;

    private void Start()
    {
        _levelManager = InjectBox.Get<LevelManager>();
        _poolManager = InjectBox.Get<PoolManager>();
        _animator = GetComponent<Animator>();
        
        EventManager.Subscribe<OnGameOverEvent>(OnGameOver);
    }

    private void OnGameOver(OnGameOverEvent obj)
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateTarget();
    }

    private void Shoot()
    {
        var arrow = _poolManager.GetOrCreate<Arrow>(_arrowPrefab, null, _shootPoint.position);
        arrow.InitArrow(_target, _damage);
    }

    private void UpdateTarget()
    {
        if (Time.time - _previousTime > _delay)
        {
            _enemies = _levelManager.GetEnemiesList();
            var shorterDistance = Mathf.Infinity;
            Enemy nearestEnemy = null;

            foreach (var enemy in _enemies)
            {
                var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shorterDistance)
                {
                    shorterDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shorterDistance <= _archerRange)
            {
                _target = nearestEnemy.transform;
                _animator.SetBool(HasTarget, true);
            }
            else
            {
                _target = null;
                _animator.SetBool(HasTarget, false);
            }
        }
    }
    
}
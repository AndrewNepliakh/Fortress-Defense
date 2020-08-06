using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fortress : MonoBehaviour, IPoolable
{
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private float _hitPoints;
    [SerializeField] private List<Sprite> _conditionSprites;

    private float _currentHitPoints;
    
    public void InitFortress()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _conditionSprites[0];
        _currentHitPoints = _hitPoints;
        
        EventManager.Subscribe<OnEnemyDoDamageEvent>(OnEnemyDoDamage);
    }

    private void OnEnemyDoDamage(OnEnemyDoDamageEvent obj)
    {
        GetDamage(obj.Damage);
    }

    public float GetDamage(float value)
    {
        _currentHitPoints -= value;
        if (_currentHitPoints < 0) EventManager.TriggerEvent<OnGameOverEvent>();

        CheckForAppearanceCondition();

        return _hitPoints;
    }

    private void CheckForAppearanceCondition()
    {
        var count = _conditionSprites.Count;
        var multiplier = 1.0f / (count - 1);

        for (int i = 0; i < count - 1; i++)
        {
            if (_currentHitPoints < _hitPoints * (1.0f - i * multiplier)) _spriteRenderer.sprite = _conditionSprites[i];
            var po = _hitPoints * (1.0f - i * multiplier);
        }

        if (_currentHitPoints < 0) _spriteRenderer.sprite = _conditionSprites[count - 1];
    }

    public void OnActivate(object argument = default) { }
    public void OnDeactivate(object argument = default) { }
}

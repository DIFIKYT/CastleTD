using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private int _maxHitPoints;
    private int _currentHitPoints;
    private Transform _transform;

    public event Action HitPointsOver;

    public int CurrentHitPoints => _currentHitPoints;
    public Transform Transform => transform;

    public void Initialize(int maxHitPoints)
    {
        _maxHitPoints = maxHitPoints;
        _currentHitPoints = _maxHitPoints;
    }

    public void TakeDamage(int value)
    {
        if (_currentHitPoints - value <= 0)
        {
            _currentHitPoints = 0;
            HitPointsOver?.Invoke();
            return;
        }

        _currentHitPoints -= value;
    }
}
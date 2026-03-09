using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _maxHitPoints;
    private int _currentHitPoints;

    public event Action HitPointsOver;

    public int CurrentHitPoints => _currentHitPoints;

    public void Initialize(int maxHitPoints)
    {
        _maxHitPoints = maxHitPoints;
        _currentHitPoints = _maxHitPoints;
    }

    public void DeacreaseHitPoints(int value)
    {
        if (_currentHitPoints - value <= 0)
        {
            _currentHitPoints = 0;
            HitPointsOver?.Invoke();
            return;
        }

        _currentHitPoints -= value;
    }

    public void InreaseHitPoints(int value)
    {
        if (_currentHitPoints + value >= _maxHitPoints)
        {
            _currentHitPoints = _maxHitPoints;
            return;
        }

        _currentHitPoints += value;
    }
}
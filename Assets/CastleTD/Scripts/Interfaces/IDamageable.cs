using System;
using UnityEngine;

public interface IDamageable
{
    Transform Transform { get; }

    public event Action HitPointsOver;

    public void TakeDamage(int damageValue);
}
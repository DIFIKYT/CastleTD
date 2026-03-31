using System.Collections;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private int _startDamageValue;
    private int _damageValue;
    private float _attackInterval;
    private IDamageable _target;

    public void Initialize(int damageValue, float attackInterval)
    {
        _startDamageValue = damageValue;
        _damageValue = _startDamageValue;
        _attackInterval = attackInterval;
    }

    public void Reset()
    {
        _target = null;
        _damageValue = _startDamageValue;
    }

    public void AddDamage(int value)
    {
        _damageValue += value;
    }

    public void ChangeTarget(IDamageable target)
    {
        _target = target;
    }

    public IEnumerator DamageCoroutine()
    {
        WaitForSeconds delay = new(_attackInterval);

        while (_target != null)
        {
            _target.TakeDamage(_damageValue);

            if (_target == null || _target.CurrentHitPoints <= 0)
                yield break;

            yield return delay;
        }
    }
}
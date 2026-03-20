using System.Collections;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private int _damageValue;
    private float _attackInterval;
    private IDamageable _target;

    public void Initialize(int damageValue, float attackInterval)
    {
        _damageValue = damageValue;
        _attackInterval = attackInterval;
    }

    public void Reset()
    {
        _target = null;
    }

    public void ChangeTarget(IDamageable target)
    {
        _target = target;
    }

    public IEnumerator DamageCoroutine()
    {
        WaitForSeconds delay = new(_attackInterval);

        do
        {
            _target.TakeDamage(_damageValue);
            yield return delay;
        } while (_target.CurrentHitPoints > 0);
    }
}
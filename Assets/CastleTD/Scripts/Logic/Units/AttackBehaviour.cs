using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private int _damageValue;
    private float _attackInterval;
    private IDamageable _target;

    public void Initialize(int damageValue)
    {
        _damageValue = damageValue;
    }

    public void ChangeTarget(IDamageable target)
    {
        _target = target;
    }

    public void Attack()
    {
        _target.TakeDamage(_damageValue);
    }
}
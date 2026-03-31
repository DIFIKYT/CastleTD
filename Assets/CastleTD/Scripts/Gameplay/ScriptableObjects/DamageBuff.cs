using UnityEngine;

[CreateAssetMenu(fileName = "New damage buff", menuName = "Buff/Create new damage buff")]
public class DamageBuff : Buff
{
    [SerializeField] private int _damageBonus;

    public override void Apply(Unit unit)
    {
        unit.AddDamage(_damageBonus);
    }
}
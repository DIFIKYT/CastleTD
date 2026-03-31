using UnityEngine;

[CreateAssetMenu(fileName = "New heal buff", menuName = "Buff/Create new heal buff")]
public class HealBuff : Buff
{
    [SerializeField] private int _healAmount;

    public override void Apply(Unit unit)
    {
        unit.Heal(_healAmount);
    }
}
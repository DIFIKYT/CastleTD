using UnityEngine;

public class PlayerUnit : Unit
{
    protected override bool IsTargetInAttackRange()
    {
        float distance = Vector3.Distance(Transform.position, CurrentTarget.Transform.position);

        return distance < UnitStats.StoppingDistance;

    }
}
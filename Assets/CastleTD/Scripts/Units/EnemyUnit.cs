using UnityEngine;

public class EnemyUnit : Unit
{
    protected override bool IsTargetInAttackRange()
    {
        float distance = Vector3.Distance(transform.position, CurrentMoveTarget.position);

        return distance < UnitStats.StoppingDistance;
    }
}
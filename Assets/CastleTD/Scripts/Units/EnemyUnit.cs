using UnityEngine;

public class EnemyUnit : Unit
{
    protected override bool IsNeedMove()
    {
        float distance = Vector3.Distance(Transform.position, CurrentTarget.position);

        return distance > UnitStats.StoppingDistance;
    }
}
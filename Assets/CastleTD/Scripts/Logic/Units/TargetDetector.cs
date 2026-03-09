using System;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public event Action<Unit> UnitDetected;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Unit unit))
        {
            UnitDetected?.Invoke(unit);
        }
    }
}
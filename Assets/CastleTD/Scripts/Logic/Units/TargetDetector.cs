using System;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public event Action<IDamageable> UnitDetected;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IDamageable damageable))
        {
            UnitDetected?.Invoke(damageable);
        }
    }
}
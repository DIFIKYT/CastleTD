using System;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public event Action<Transform, IDamageable> TargetDetected;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.TryGetComponent(out IDamageable damageable))
        {
            TargetDetected?.Invoke(collider.gameObject.transform, damageable);
        }
    }
}
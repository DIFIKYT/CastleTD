using UnityEngine;

[RequireComponent(typeof(Health))]
public class Gates : MonoBehaviour
{
    [SerializeField] private int _maxHitPoints;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.Initialize(_maxHitPoints);
    }
}
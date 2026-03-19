using UnityEngine;

[RequireComponent(typeof(Health))]
public class Gates : MonoBehaviour, IFactionMember
{
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private Faction _faction;

    private Health _health;

    public Faction Faction => _faction;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.Initialize(_maxHitPoints);
    }
}
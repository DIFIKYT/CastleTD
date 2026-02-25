using UnityEngine;

public abstract class AbstractUnit : MonoBehaviour
{
    [SerializeField] private UnitMover _unitMover;
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private AttackBehaviour _attackBehaviour;
    [SerializeField] private Health _health;

    [SerializeField] private Transform _target;

    private void Start()
    {
        _unitMover.GetTarget(_target);
    }
}
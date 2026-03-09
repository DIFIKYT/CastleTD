using UnityEngine;

[RequireComponent(typeof(Mover))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] private UnitStats _unitStats;
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private AttackBehaviour _attackBehaviour;
    [SerializeField] private Health _health;
    [SerializeField] private Transform _startTarget;

    private Mover _mover;
    private Transform _transform;
    private Transform _currentTarget;
    private bool _isFighting;

    protected UnitStats UnitStats => _unitStats;
    protected Transform Transform => _transform;
    protected Transform CurrentTarget => _currentTarget;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _currentTarget = _startTarget;
        _transform = transform;
        _mover.ChangeTarget(_currentTarget);
        _mover.Initialize(_transform, _unitStats.MoveSpeed, _unitStats.RotateSpeed);
    }

    private void OnEnable()
    {
        _targetDetector.UnitDetected += OnUnitDetected;
    }

    private void OnDisable()
    {
        _targetDetector.UnitDetected -= OnUnitDetected;
    }

    private void Update()
    {
        if (_currentTarget == null)
            return;

        if (IsNeedMove())
        {
            MoveToTarget();
        }
    }

    private void OnUnitDetected(Unit unit)
    {
        if (_isFighting)
            return;

        SwitchTarget(unit.gameObject.transform);
    }

    private void MoveToTarget()
    {
        _mover.MoveToTarget();
    }

    private void SwitchTarget(Transform newTarget)
    {
        _currentTarget = newTarget;
        _mover.ChangeTarget(newTarget);
        _isFighting = true;
    }

    protected abstract bool IsNeedMove();
}
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AttackBehaviour))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] private UnitStats _unitStats;
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private Transform _startTargetTransform;

    private AttackBehaviour _attackBehaviour;
    private Health _health;
    private Mover _mover;
    private IDamageable _startTarget;
    private IDamageable _currentTarget;
    private bool _isFighting;

    protected UnitStats UnitStats => _unitStats;
    protected Transform Transform => transform;
    protected IDamageable CurrentTarget => _currentTarget;

    private void Awake()
    {
        _startTarget = _startTargetTransform.GetComponent<IDamageable>();
        _currentTarget = _startTarget;

        _mover = GetComponent<Mover>();
        _mover.Initialize(_unitStats.MoveSpeed, _unitStats.RotateSpeed);
        _mover.ChangeTarget(_currentTarget.Transform);

        _attackBehaviour = GetComponent<AttackBehaviour>();
        _attackBehaviour.Initialize(_unitStats.DamageValue);
        _attackBehaviour.ChangeTarget(_currentTarget);

        _health = GetComponent<Health>();
        _health.Initialize(_unitStats.MaxHitPoints);
    }

    private void OnEnable()
    {
        _targetDetector.UnitDetected += OnUnitDetected;
        _health.HitPointsOver += OnHitPointsOver;
    }

    private void OnDisable()
    {
        _targetDetector.UnitDetected -= OnUnitDetected;
        _health.HitPointsOver -= OnHitPointsOver;
    }

    private void Update()
    {
        if (_currentTarget == null)
            return;

        if (IsTargetInAttackRange())
        {
            _isFighting = true;
            AttackTarget();
        }
        else
        {
            MoveToTarget();
        }
    }

    private void OnUnitDetected(IDamageable damageable)
    {
        if (_isFighting)
            return;

        ChangeTargetToNew(damageable);
    }

    public void OnHitPointsOver()
    {
        Destroy(gameObject);
    }

    public void OnTargetHitPointsOver()
    {
        ChangeTargetToStart();
    }

    private void MoveToTarget()
    {
        _mover.MoveToTarget();
    }

    private void AttackTarget()
    {
        _attackBehaviour.Attack();
    }

    private void ChangeTargetToNew(IDamageable newTarget)
    {
        _currentTarget = newTarget;
        _mover.ChangeTarget(_currentTarget.Transform);
        _attackBehaviour.ChangeTarget(_currentTarget);

        _currentTarget.HitPointsOver += OnTargetHitPointsOver;
    }

    private void ChangeTargetToStart()
    {
        _currentTarget = _startTarget;
        _mover.ChangeTarget(_currentTarget.Transform);
        _attackBehaviour.ChangeTarget(_currentTarget);

        _currentTarget.HitPointsOver -= OnTargetHitPointsOver;
    }

    protected abstract bool IsTargetInAttackRange();
}
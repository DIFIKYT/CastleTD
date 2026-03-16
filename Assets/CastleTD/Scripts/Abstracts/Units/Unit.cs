using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AttackBehaviour))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] private UnitStats _unitStats;
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private Transform _startMoveTarget;

    private AttackBehaviour _attackBehaviour;
    private Health _health;
    private Mover _mover;
    private IDamageable _startAttackTarget;
    private Transform _currentMoveTarget;
    private IDamageable _currentAttackTarget;
    private UnitState _state;
    private Coroutine _attackCoroutine;

    protected UnitStats UnitStats => _unitStats;
    protected Transform CurrentMoveTarget => _currentMoveTarget;

    private void Awake()
    {
        if (_startMoveTarget.TryGetComponent(out IDamageable damageable) == false)
        {
            throw new NullReferenceException($"{_startMoveTarget} does not contain IDamageable");
        }

        _startAttackTarget = damageable;
        _currentMoveTarget = _startMoveTarget;
        _currentAttackTarget = _startAttackTarget;

        _mover = GetComponent<Mover>();
        _mover.Initialize(_unitStats.MoveSpeed, _unitStats.RotateSpeed);
        _mover.ChangeTarget(_currentMoveTarget);

        _attackBehaviour = GetComponent<AttackBehaviour>();
        _attackBehaviour.Initialize(_unitStats.DamageValue, _unitStats.AttackInterval);
        _attackBehaviour.ChangeTarget(_currentAttackTarget);

        _health = GetComponent<Health>();
        _health.Initialize(_unitStats.MaxHitPoints);

        _state = UnitState.Moving;
    }

    private void OnEnable()
    {
        _targetDetector.TargetDetected += OnTargetDetected;
        _health.HitPointsOver += OnHitPointsOver;
    }

    private void OnDisable()
    {
        _targetDetector.TargetDetected -= OnTargetDetected;
        _health.HitPointsOver -= OnHitPointsOver;
    }

    private void Update()
    {
        if (_currentMoveTarget == null)
            return;

        if (_state == UnitState.Fighting)
            return;

        if (IsTargetInAttackRange())
        {
            _state = UnitState.Fighting;
            _attackCoroutine = StartCoroutine(_attackBehaviour.DamageCoroutine());
            return;
        }

        _mover.MoveToTarget();
    }

    private void OnTargetDetected(Transform moveTarget, IDamageable attackTarget)
    {
        if (_state == UnitState.Fighting || _currentMoveTarget != _startMoveTarget)
            return;

        ChangeTargetToNew(moveTarget, attackTarget);
    }

    private void OnHitPointsOver()
    {
        Destroy(gameObject);
    }

    private void OnTargetHitPointsOver()
    {
        ChangeTargetToNew(_startMoveTarget, _startAttackTarget);
    }

    private void ChangeTargetToNew(Transform moveTarget, IDamageable attackTarget)
    {
        _currentAttackTarget.HitPointsOver -= OnTargetHitPointsOver;

        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);

        _currentMoveTarget = moveTarget;
        _currentAttackTarget = attackTarget;
        _state = UnitState.Moving;
        _mover.ChangeTarget(_currentMoveTarget);
        _attackBehaviour.ChangeTarget(_currentAttackTarget);

        _currentAttackTarget.HitPointsOver += OnTargetHitPointsOver;
    }

    protected abstract bool IsTargetInAttackRange();
}
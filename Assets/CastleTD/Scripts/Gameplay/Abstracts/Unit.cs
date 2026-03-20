using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AttackBehaviour))]
public abstract class Unit : MonoBehaviour, IFactionMember, ISpawnable
{
    [SerializeField] private UnitStats _unitStats;
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private Transform _startMoveTarget;
    [SerializeField] private Faction _faction;

    private AttackBehaviour _attackBehaviour;
    private Health _health;
    private Mover _mover;
    private IDamageable _startAttackTarget;
    private Transform _currentMoveTarget;
    private IDamageable _currentAttackTarget;
    private UnitState _state;
    private Coroutine _attackCoroutine;

    public event Action<Unit> Died;

    public Faction Faction => _faction;

    private void Awake()
    {
        if (_startMoveTarget.TryGetComponent(out IDamageable damageable) == false)
        {
            throw new NullReferenceException($"{gameObject.name}: {_startMoveTarget.gameObject.name} does not contain IDamageable");
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

    public void OnSpawn()
    {
        _mover.Reset();
        _attackBehaviour.Reset();
        _health.Reset();

        _targetDetector.TargetDetected += OnTargetDetected;
        _health.HitPointsOver += OnHitPointsOver;

        _currentMoveTarget = _startMoveTarget;
        _currentAttackTarget = _startAttackTarget;

        _mover.ChangeTarget(_currentMoveTarget);
        _attackBehaviour.ChangeTarget(_currentAttackTarget);
    }

    public void OnDespawn()
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
        if (CanChangeTarget() == false)
            return;

        if (moveTarget.TryGetComponent(out IFactionMember factionMember))
            if (factionMember.Faction == _faction)
                return;

        ChangeTarget(moveTarget, attackTarget);
    }

    private bool CanChangeTarget()
    {
        if (_state == UnitState.Fighting)
            return false;

        if (_state == UnitState.Moving && _currentMoveTarget != _startMoveTarget)
            return false;

        return true;
    }

    private void OnHitPointsOver()
    {
        Died?.Invoke(this);
    }

    private void OnTargetHitPointsOver()
    {
        ChangeTarget(_startMoveTarget, _startAttackTarget);
    }

    private void ChangeTarget(Transform moveTarget, IDamageable attackTarget)
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

    private bool IsTargetInAttackRange()
    {
        float distance = (transform.position - _currentMoveTarget.position).sqrMagnitude;
        float range = _unitStats.StoppingDistance * _unitStats.StoppingDistance;

        return distance < range;
    }
}
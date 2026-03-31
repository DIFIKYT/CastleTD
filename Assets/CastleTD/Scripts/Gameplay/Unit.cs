using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AttackBehaviour))]
public class Unit : MonoBehaviour, IFactionMember, ISpawnable
{
    [SerializeField] private TargetDetector _targetDetector;

    private Faction _faction;
    private AttackBehaviour _attackBehaviour;
    private Health _health;
    private Mover _mover;
    private Transform _startMoveTarget;
    private IDamageable _startAttackTarget;
    private Transform _currentMoveTarget;
    private IDamageable _currentAttackTarget;
    private UnitState _state;
    private Coroutine _attackCoroutine;

    public event Action<Unit> Died;

    public Faction Faction => _faction;
    public UnitConfig Config { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _attackBehaviour = GetComponent<AttackBehaviour>();
        _health = GetComponent<Health>();
    }

    public void Initialize(UnitConfig config, Faction faction, Transform startMoveTarget, IDamageable startAttackTarget)
    {
        _faction = faction;
        Config = config;

        _mover.Initialize(Config.Stats.MoveSpeed, Config.Stats.RotateSpeed);
        _attackBehaviour.Initialize(Config.Stats.DamageValue, Config.Stats.AttackInterval);
        _health.Initialize(Config.Stats.MaxHitPoints);

        _startMoveTarget = startMoveTarget;
        _startAttackTarget = startAttackTarget;
    }

    public void OnSpawn()
    {
        ChangeTarget(_startMoveTarget, _startAttackTarget);

        _state = UnitState.Moving;

        _targetDetector.TargetDetected += OnTargetDetected;
        _health.HitPointsOver += OnHitPointsOver;
    }

    public void OnDespawn()
    {
        _mover.Reset();
        _attackBehaviour.Reset();
        _health.Reset();

        _targetDetector.TargetDetected -= OnTargetDetected;
        _health.HitPointsOver -= OnHitPointsOver;
    }

    public void AddDamage(int value)
    {
        _attackBehaviour.AddDamage(value);
    }

    public void Heal(int value)
    {
        _health.Heal(value);
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
        if (_currentAttackTarget != null)
        {
            _currentAttackTarget.HitPointsOver -= OnTargetHitPointsOver;
        }

        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackBehaviour = null;
        }

        _currentMoveTarget = moveTarget;
        _currentAttackTarget = attackTarget;
        _state = UnitState.Moving;
        _mover.ChangeTarget(_currentMoveTarget);

        if (_currentAttackTarget != null)
        {
            _attackBehaviour.ChangeTarget(_currentAttackTarget);
            _currentAttackTarget.HitPointsOver += OnTargetHitPointsOver;
        }
    }

    private bool IsTargetInAttackRange()
    {
        float distance = (transform.position - _currentMoveTarget.position).sqrMagnitude;
        float range = Config.Stats.StoppingDistance * Config.Stats.StoppingDistance;

        return distance < range;
    }
}
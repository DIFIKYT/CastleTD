using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private int _startObjectsCount;

    private Dictionary<Unit, ObjectPool<Unit>> _pools = new();

    public Unit SpawnUnit(UnitConfig config, Vector3 spawnPoint, Transform parent, Faction faction, Transform startMoveTarget, IDamageable startAttackTarget)
    {
        if (_pools.ContainsKey(config.Prefab) == false)
        {
            _pools[config.Prefab] = new(config.Prefab, parent, _startObjectsCount);
        }

        Unit unit = _pools[config.Prefab].Get();
        unit.transform.SetPositionAndRotation(spawnPoint, Quaternion.Euler(0f, parent.rotation.eulerAngles.y, 0f));
        unit.Initialize(config.Prefab, config.Stats, faction, startMoveTarget, startAttackTarget);
        unit.OnSpawn();
        unit.Died += OnDied;

        return unit;
    }

    private void OnDied(Unit unit)
    {
        unit.Died -= OnDied;
        unit.OnDespawn();
        _pools[unit.Prefab].Release(unit);
    }
}
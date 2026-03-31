using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private List<Vector3> _spawnPoints;
    [SerializeField] private int _spawnInterval;
    [SerializeField] private Transform _startMoveTarget;
    [SerializeField] private Faction _faction;

    [Header("Resources")]
    [SerializeField] private ResourceStorage _resourceStorage;

    [Header("Structures")]
    [SerializeField] private List<ResourceProductionStructure> _resourceProductionStructures;
    [SerializeField] private List<BuffStructure> _buffStructures;
    [SerializeField] private List<UnitTrainingStructure> _unitTrainingStructures;

    [Header("Units")]
    [SerializeField] private Garrison _garrison;

    public void TakeStructure(Structure structure)
    {
        switch (structure)
        {
            case ResourceProductionStructure:
                AddResourceProductionStructure(structure);
                break;
            case BuffStructure:
                AddBuffStructure(structure);
                break;
            case UnitTrainingStructure:
                AddUnitTrainingStructure(structure);
                break;
        }
    }

    private void AddResourceProductionStructure(Structure structure)
    {
        ResourceProductionStructure newStructure = (ResourceProductionStructure)structure;
        _resourceProductionStructures.Add(newStructure);
        newStructure.ResourceProduced += OnResourceProduced;
    }

    private void AddBuffStructure(Structure structure)
    {
        BuffStructure newStructure = (BuffStructure)structure;
        _buffStructures.Add(newStructure);
        newStructure.BuffPrepared += OnBuffPrepared;
    }

    private void AddUnitTrainingStructure(Structure structure)
    {
        UnitTrainingStructure newStructure = (UnitTrainingStructure)structure;
        _unitTrainingStructures.Add(newStructure);
        newStructure.TrainigCompleted += OnTrainigCompleted;
    }

    private void OnResourceProduced(ResourceType resource, int amount)
    {
        _resourceStorage.GetResource(resource, amount);
    }

    private void OnBuffPrepared(Buff buff)
    {
        BuffUnits(buff);
    }

    private void OnTrainigCompleted(UnitConfig config)
    {
        SpawnUnit(config);
    }

    private void OnDied(Unit unit)
    {
        unit.Died -= OnDied;
        _garrison.RemoveUnit(unit);
    }

    private void SpawnUnit(UnitConfig config)
    {
        Unit unit = _unitSpawner.SpawnUnit(config, _spawnPoints[Random.Range(0, _spawnPoints.Count)], transform, _faction, _startMoveTarget, null);
        _garrison.AddUnit(unit);
        unit.Died += OnDied;
    }

    private void BuffUnits(Buff buff)
    {
        foreach (Unit unit in _garrison.ActiveUnits)
            unit.TakeBuff(buff);
    }
}
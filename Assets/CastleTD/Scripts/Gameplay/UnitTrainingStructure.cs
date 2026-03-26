using System;
using UnityEngine;

public class UnitTrainingStructure : Structure
{
    [SerializeField] private UnitConfig _unitConfig;

    public event Action<Unit> TrainigCompleted;

    protected override void OnProcessComplete()
    {
        TrainigCompleted?.Invoke(_unitConfig.Prefab);
    }
}
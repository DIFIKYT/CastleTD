using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCamp : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private List<Vector3> _spawnPoints;
    [SerializeField] private int _spawnInterval;
    [SerializeField] private Transform _startMoveTarget;
    [SerializeField] private Faction _faction;

    [Header("Units")]
    [SerializeField] private List<UnitConfig> _configs;
}
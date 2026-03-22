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

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        WaitForSeconds delay = new(_spawnInterval);

        while (true)
        {
            _unitSpawner.SpawnUnit(_configs[Random.Range(0, _configs.Count)], _spawnPoints[Random.Range(0, _spawnPoints.Count)], transform, _faction, _startMoveTarget, null);

            yield return delay;
        }
    }
}
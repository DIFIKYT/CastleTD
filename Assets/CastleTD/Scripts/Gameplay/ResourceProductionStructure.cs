using System;
using UnityEngine;

public class ResourceProductionStructure : Structure
{
    private const int BaseModifier = 1;

    [SerializeField] private ResourceType _resourceType;

    private int _level = 1;
    private int _currentModifier;

    public event Action<ResourceType, int> ResourceProduced;

    private void Awake()
    {
        _currentModifier = BaseModifier;
    }

    protected override void OnProcessComplete()
    {
        ResourceProduced?.Invoke(_resourceType, _level * _currentModifier);
    }
}
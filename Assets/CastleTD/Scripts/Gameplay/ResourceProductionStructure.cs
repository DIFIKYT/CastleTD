using System;
using UnityEngine;

public class ResourceProductionStructure : Structure
{
    [SerializeField] private ResourceType _resourceType;

    private int _level = 1;

    public event Action<ResourceType, int> ResourceProduced;

    protected override void OnProcessComplete()
    {
        ResourceProduced?.Invoke(_resourceType, _level);
    }
}
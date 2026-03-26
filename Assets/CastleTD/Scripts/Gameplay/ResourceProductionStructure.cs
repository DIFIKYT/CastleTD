using System;
using UnityEngine;

public class ResourceProductionStructure : Structure
{
    [SerializeField] private Resource _resourceType;

    private int _level = 1;
    private float _resourceModifier = 1f;

    public event Action<Resource, int> ResourceProduced;

    protected override void OnProcessComplete()
    {
        int amount = (int)(_level * _resourceModifier);

        ResourceProduced?.Invoke(_resourceType, amount);
    }
}
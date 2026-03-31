using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private Dictionary<ResourceType, int> _resources;

    public void GetResource(ResourceType resource, int amount)
    {
        _resources[resource] += amount;
    }
}
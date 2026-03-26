using System;
using UnityEngine;

public class BlessStructure : Structure
{
    [SerializeField] private Blessing _blessing;

    public event Action<Blessing> PraygeCompleted;

    protected override void OnProcessComplete()
    {
        PraygeCompleted?.Invoke(_blessing);
    }
}
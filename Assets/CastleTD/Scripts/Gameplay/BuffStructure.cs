using System;
using UnityEngine;

public class BuffStructure : Structure
{
    [SerializeField] private Buff _buff;

    public event Action<Buff> BuffPrepared;

    protected override void OnProcessComplete()
    {
        BuffPrepared?.Invoke(_buff);
    }
}
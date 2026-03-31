using System;
using UnityEngine;

public class BuffStructure : Structure
{
    [SerializeField] private BuffType _buff;

    public event Action<BuffType> BuffPrepared;

    protected override void OnProcessComplete()
    {
        BuffPrepared?.Invoke(_buff);
    }
}
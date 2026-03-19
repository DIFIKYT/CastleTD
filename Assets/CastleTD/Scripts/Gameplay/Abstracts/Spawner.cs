using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _startObjectsCount;

    protected ObjectPool<T> _pool;

    protected virtual void Awake()
    {
        _pool = new(_prefab, transform, _startObjectsCount);
    }

    public virtual T Spawn()
    {
        T item = _pool.Get();
        return item;
    }

    public virtual void Despawn(T item)
    {
        _pool.Release(item);
    }
}
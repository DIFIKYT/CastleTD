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

    protected T Spawn(Vector3 position)
    {
        T item = _pool.Get();
        item.transform.position = position;
        item.OnSpawn();
        return item;
    }

    protected void Despawn(T item)
    {
        item.OnDespawn();
        _pool.Release(item);
    }
}
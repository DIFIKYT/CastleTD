using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _parent;
    private Queue<T> _pool;

    public ObjectPool(T prefab, Transform parent, int startObjectsCount)
    {
        _prefab = prefab;
        _parent = parent;
        _pool = new();

        for (int i = 0; i < startObjectsCount; i++)
        {
            Create();
        }
    }

    public T Get()
    {
        if (_pool.Any() == false)
            Create();

        T item = _pool.Dequeue();
        item.gameObject.SetActive(true);

        return item;
    }

    public void Release(T item)
    {
        _pool.Enqueue(item);
        item.gameObject.SetActive(false);
    }

    private T Create()
    {
        T item = GameObject.Instantiate(_prefab, _parent);
        _pool.Enqueue(item);
        item.gameObject.SetActive(false);

        return item;
    }
}
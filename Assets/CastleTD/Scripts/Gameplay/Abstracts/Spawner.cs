using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] private ISpawnable _spawnable;

    public abstract void Spawn();
}
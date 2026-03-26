using System.Collections;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    [SerializeField] private float _processTime;

    private Coroutine _processCoroutine;

    public void StartProcess()
    {
        if (_processCoroutine != null)
            return;

        _processCoroutine = StartCoroutine(Process());
    }

    public void StopProcess()
    {
        StopCoroutine(_processCoroutine);

        _processCoroutine = null;
    }

    protected IEnumerator Process()
    {
        yield return new WaitForSeconds(_processTime);

        OnProcessComplete();

        _processCoroutine = null;
    }

    protected abstract void OnProcessComplete();
}
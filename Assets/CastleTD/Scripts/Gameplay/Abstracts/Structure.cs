using System.Collections;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    [SerializeField] private float _processTime;

    private Coroutine _processCoroutine;

    protected IEnumerator Process()
    {
        yield return new WaitForSeconds(_processTime);

        OnProcessComplete();

        _processCoroutine = null;
    }

    protected void StartProcess()
    {
        if (_processCoroutine != null)
            return;

        _processCoroutine = StartCoroutine(Process());
    }

    protected abstract void OnProcessComplete();
}
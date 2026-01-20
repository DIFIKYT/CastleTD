using UnityEngine;

[RequireComponent(typeof(SceneSwitcher))]
public class Game : MonoBehaviour
{
    private SceneSwitcher _sceneSwitcher;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _sceneSwitcher = GetComponent<SceneSwitcher>();
        _sceneSwitcher.LoadMainMenu();
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private SceneManager _sceneManager;

    private void Awake()
    {
        _sceneManager = new();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
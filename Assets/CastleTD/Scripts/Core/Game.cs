using UnityEngine;

[RequireComponent(typeof(SceneSwitcher))]
public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    private GameState _currentState;

    private SceneSwitcher _sceneSwitcher;

    private void Awake()
    {
        _sceneSwitcher = GetComponent<SceneSwitcher>();
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetState(GameState.MainMenu);
    }

    public void SetState(GameState newState)
    {
        if(_currentState == newState)
            return;

        _currentState = newState;
        ChangeCurrentScene(newState);
    }

    private void ChangeCurrentScene(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                _sceneSwitcher.LoadMainMenu();
                break;
            case GameState.Game:
                _sceneSwitcher.LoadGame();
                break;
            case GameState.Pause:
                break;
            case GameState.GameOver:
                break;
        }
    }
}
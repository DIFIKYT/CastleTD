using UnityEngine;
using UnityEngine.UI;

public class MenuPanelSwitcher : MonoBehaviour
{
    [Header("Main menu")]
    [SerializeField] private Canvas _mainMenu;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _settingsButton;

    [Header("Leaderboard")]
    [SerializeField] private Canvas _leaderboard;
    [SerializeField] private Button _leaderboardBackButton;

    [Header("Settings")]
    [SerializeField] private Canvas _settings;
    [SerializeField] private Button _settingsBackButton;

    private Canvas _activePanel;

    private void OnEnable()
    {
        _leaderboardButton.onClick.AddListener(ShowLeaderboard);
        _settingsButton.onClick.AddListener(ShowSettings);
        _leaderboardBackButton.onClick.AddListener(ShowMainMenu);
        _settingsBackButton.onClick.AddListener(ShowMainMenu);
    }

    private void OnDisable()
    {
        _leaderboardButton.onClick.RemoveListener(ShowLeaderboard);
        _settingsButton.onClick.RemoveListener(ShowSettings);
        _leaderboardBackButton.onClick.RemoveListener(ShowMainMenu);
        _settingsBackButton.onClick.RemoveListener(ShowMainMenu);
    }

    private void Awake()
    {
        _activePanel = _mainMenu;
        _activePanel.gameObject.SetActive(true);
    }

    private void ChangeActive(Canvas panel)
    {
        _activePanel.gameObject.SetActive(false);
        _activePanel = panel;
        _activePanel.gameObject.SetActive(true);
    }

    private void ShowMainMenu()
    {
        ChangeActive(_mainMenu);
    }

    private void ShowLeaderboard()
    {
        ChangeActive(_leaderboard);
    }

    private void ShowSettings()
    {
        ChangeActive(_settings);
    }
}
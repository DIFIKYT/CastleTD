using UnityEngine;
using UnityEngine.UI;

public class MenuButtonsListener : MonoBehaviour
{
    [Header("Other")]
    [SerializeField] private MenuPanelSwitcher _menuPanelSwitcher;

    [Header("Main menu")]
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _settingsButton;
    
    [Header("Leaderboard")]
    [SerializeField] private Button _leaderboardBackButton;
    
    [Header("Settings")]
    [SerializeField] private Button _settingsBackButton;
    
    private void OnEnable()
    {
        _newGameButton.onClick.AddListener(OnNewGameClicked);
        _continueButton.onClick.AddListener(OnContinueClicked);
        _leaderboardButton.onClick.AddListener(_menuPanelSwitcher.ShowLeaderboard);
        _settingsButton.onClick.AddListener(_menuPanelSwitcher.ShowSettings);
        _leaderboardBackButton.onClick.AddListener(_menuPanelSwitcher.ShowMainMenu);
        _settingsBackButton.onClick.AddListener(_menuPanelSwitcher.ShowMainMenu);
    }

    private void OnDisable()
    {        
        _newGameButton.onClick.RemoveListener(OnNewGameClicked);
        _continueButton.onClick.RemoveListener(OnContinueClicked);
        _leaderboardButton.onClick.RemoveListener(_menuPanelSwitcher.ShowLeaderboard);
        _settingsButton.onClick.RemoveListener(_menuPanelSwitcher.ShowSettings);
        _leaderboardBackButton.onClick.RemoveListener(_menuPanelSwitcher.ShowMainMenu);
        _settingsBackButton.onClick.RemoveListener(_menuPanelSwitcher.ShowMainMenu);
    }

    private void OnNewGameClicked()
    {
        Game.Instance.SetState(GameState.Game);
    }

    private void OnContinueClicked()
    {
        Game.Instance.SetState(GameState.Game);
    }
}
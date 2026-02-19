using UnityEngine;

public class MenuPanelSwitcher : MonoBehaviour
{
    [SerializeField] private Canvas _mainMenu;
    [SerializeField] private Canvas _leaderboard;
    [SerializeField] private Canvas _settings;

    private Canvas _activePanel;

    private void Awake()
    {
        _activePanel = _mainMenu;
        _activePanel.gameObject.SetActive(true);
    }

    public void ShowMainMenu()
    {
        ChangeActive(_mainMenu);
    }

    public void ShowLeaderboard()
    {
        ChangeActive(_leaderboard);
    }

    public void ShowSettings()
    {
        ChangeActive(_settings);
    }

    private void ChangeActive(Canvas panel)
    {
        _activePanel.gameObject.SetActive(false);
        _activePanel = panel;
        _activePanel.gameObject.SetActive(true);
    }
}
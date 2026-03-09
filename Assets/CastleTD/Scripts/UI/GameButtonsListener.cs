using UnityEngine;
using UnityEngine.UI;

public class GameButtonsListener : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    
    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackClicked);
    }

    private void OnDisable()
    {        
        _backButton.onClick.RemoveListener(OnBackClicked);
    }

    private void OnBackClicked()
    {
        Game.Instance.SetState(GameState.MainMenu);
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private List<string> _languages;
    [SerializeField] private Button _button;

    private Queue<string> _languagesQueue;

    private void Awake()
    {
        _languagesQueue = new();

        foreach (string language in _languages)
        {
            _languagesQueue.Enqueue(language);
        }
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Switch);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Switch);
    }

    private void Switch()
    {
        YG2.SwitchLanguage(_languagesQueue.Peek());
        _languagesQueue.Enqueue(_languagesQueue.Dequeue());
    }
}
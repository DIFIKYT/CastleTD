using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using YG;

public class LanguageSwitcher : MonoBehaviour
{
    [ProButton]
    public void ChangeLanguageOnTurkish()
    {
        YG2.SwitchLanguage("tr");
    }

    [ProButton]
    public void ChangeLanguageOnEnglish()
    {
        YG2.SwitchLanguage("en");
    }

    [ProButton]
    public void ChangeLanguageOnRussian()
    {
        YG2.SwitchLanguage("ru");
    }
}
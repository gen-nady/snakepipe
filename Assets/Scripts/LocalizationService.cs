using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationService : Singleton<LocalizationService>
{
    [SerializeField]
    private Language language;

    private Dictionary<Language, string> _languageContainer = new Dictionary<Language, string>()
    {
        { Language.English, "English"},
        { Language.Russian, "Russia"},
        { Language.Turkish, "Turkish"}
    };

    private void OnValidate()
    {
        I2.Loc.LocalizationManager.CurrentLanguage = _languageContainer[language];
    }

    public void SetLanguage(Language language)
    {
        I2.Loc.LocalizationManager.CurrentLanguage = _languageContainer[language];
    }
}

public enum Language
{
    English,
    Russian,
    Turkish
}

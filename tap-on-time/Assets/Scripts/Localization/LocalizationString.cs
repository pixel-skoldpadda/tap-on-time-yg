using System;
using UnityEngine;
using YG;

namespace Localization
{
    [Serializable]
    public class LocalizationString
    {
        [SerializeField] private Translation[] translations;

        public string Get()
        {
            string lang = YandexGame.lang;
            foreach (Translation translation in translations)
            {
                if (translation.Language.ToString().Equals(lang))
                {
                    return translation.Value;
                }
            }
            throw new Exception($"Localization string for language: {lang} not found!");
        }
    }
}
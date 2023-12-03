using Configs;
using TMPro;
using UnityEngine;
using YG;

namespace UI.Hud
{
    public class LanguageContainer : BaseHudContainer
    {
        [SerializeField] private TextMeshProUGUI currentLanguage;

        private void Start()
        {
            currentLanguage.text = YandexGame.savesData.language;
        }

        // TODO 100% можно оптимизировать
        public void ChangeLanguage()
        {
            string currentLang = YandexGame.savesData.language;
            string[] langs = GameConfig.Langs;
            
            for (var i = 0; i < langs.Length; i++)
            {
                if (currentLang.Equals(langs[i]))
                {
                    int nextLangIndex = i + 1;
                    if (nextLangIndex > langs.Length - 1)
                    {
                        nextLangIndex = 0;
                    }

                    currentLang = langs[nextLangIndex];
                    YandexGame.SwitchLanguage(currentLang);
                    currentLanguage.text = currentLang;
                    return;
                }
            }
        }
    }
}
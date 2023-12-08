using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Ui.Windows.SettingsWindow
{
    public class SettingsWindow : Window
    {
        [SerializeField] private Toggle enToggle;
        [SerializeField] private Toggle ruToggle;
        [SerializeField] private Button loginButton;
        
        protected override void Awake()
        {
            base.Awake();
            
            string language = YandexGame.savesData.language;
            switch (language)
            {
                case "en":
                    enToggle.isOn = true;
                    break;
                case "ru":
                    ruToggle.isOn = true;
                    break;
                default:
                    enToggle.isOn = true;
                    break;
            }

            loginButton.interactable = !YandexGame.auth;
        }

        public void OnLanguageChanged(string language)
        {
            YandexGame.SwitchLanguage(language);
        }

        public void OpenAuthDialog()
        {
            YandexGame.RequestAuth();
            Close();
        }
    }
}
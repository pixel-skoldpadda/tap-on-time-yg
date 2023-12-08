using Infrastructure.Services.WindowsManager;
using Ui.Windows;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.Hud
{
    public class SettingsContainer : BaseHudContainer
    {
        [SerializeField] private Button settingsButton;
        
        private IWindowsManager _windows;
        
        public void Construct(IWindowsManager windows)
        {
            _windows = windows;
        }
        
        public void OpenSettingsWindow()
        {
            settingsButton.interactable = false;
            YandexGame.savesData.GamePaused = true;
            _windows.OpenWindow(WindowType.Settings, false, () =>
            {
                settingsButton.interactable = true;
                YandexGame.savesData.GamePaused = false;
            });
        }
    }
}
using Infrastructure.Services.WindowsManager;
using Ui.Windows;
using UnityEngine;
using YG;
using Zenject;
using Button = UnityEngine.UI.Button;

namespace UI.Hud
{
    public class TasksContainer : BaseHudContainer
    {
        [SerializeField] private Button _tasksButton;
        
        private IWindowsManager _windows;
        private DiContainer _container;
        
        public void Construct(IWindowsManager windows, DiContainer container)
        {
            _windows = windows;
            _container = container;
        }

        public void OnButtonCLicked()
        {
            _tasksButton.interactable = false;
            YandexGame.savesData.GamePaused = true;

            //: TODO Доработать корректное скрытие HUD
            Hud hud = _container.Resolve<Hud>();
            hud.SettingsContainer.Hide();
            hud.GemsContainer.Hide();

            _windows.OpenWindow(WindowType.DailyTasks, false, () =>
            {
                YandexGame.savesData.GamePaused = false;
                _tasksButton.interactable = true;
                
                hud.SettingsContainer.Show();
                hud.GemsContainer.Show();
            });
        }
    }
}
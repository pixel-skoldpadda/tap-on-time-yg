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
        [SerializeField] private Button tasksButton;
        
        private IWindowsManager _windows;
        private DiContainer _container;
        
        public void Construct(IWindowsManager windows, DiContainer container)
        {
            _windows = windows;
            _container = container;
        }

        public void OnButtonCLicked()
        {
            tasksButton.interactable = false;
            YandexGame.savesData.GamePaused = true;
            
            Hud hud = _container.Resolve<Hud>();
            hud.Hide();

            _windows.OpenWindow(WindowType.DailyTasks, false, () =>
            {
                YandexGame.savesData.GamePaused = false;
                tasksButton.interactable = true;
                hud.Show();
            });
        }
    }
}
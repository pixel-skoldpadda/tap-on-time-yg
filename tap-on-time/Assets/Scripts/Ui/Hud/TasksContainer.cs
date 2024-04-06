using System;
using DailyTasks;
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
        [SerializeField] private GameObject iconNew;
        
        private IWindowsManager _windows;
        private DiContainer _container;
        
        public void Construct(IWindowsManager windows, DiContainer container)
        {
            _windows = windows;
            _container = container;

            UpdateIconNewVisibility();
            
            SavesYG state = YandexGame.savesData;
            state.OnTaskCompleted += UpdateIconNewVisibility;
            state.OnTaskPrizeClaimed += UpdateIconNewVisibility;
        }

        private void UpdateIconNewVisibility()
        {
            foreach (DailyTask task in YandexGame.savesData.Tasks)
            {
                if (task.Completed && !task.PrizeClaimed)
                {
                    iconNew.SetActive(true);
                    return;
                }
            }
            iconNew.SetActive(false);
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

        private void OnDestroy()
        {
            SavesYG state = YandexGame.savesData;
            state.OnTaskCompleted -= UpdateIconNewVisibility;
            state.OnTaskPrizeClaimed -= UpdateIconNewVisibility;
        }
    }
}
using System.Collections.Generic;
using DailyTasks;
using Infrastructure.Services.Items;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.WindowsManager;
using UnityEngine;
using YG;
using Zenject;

namespace Ui.Windows.TasksWindow
{
    public class DailyTasksWindow : Window
    {
        [SerializeField] private GameObject tasksViewPrefab;
        [SerializeField] private Transform tasksContainer;
        
        private IItemsService _items;
        private ISaveLoadService _saveLoadService;

        [Inject]
        protected void Construct(IWindowsManager windowsManager, ISaveLoadService saveLoadService)
        {
            base.Construct(windowsManager);

            _saveLoadService = saveLoadService;
            InitTasks();
        }
        
        private void InitTasks()
        {
            List<DailyTask> tasks = YandexGame.savesData.Tasks;
            foreach (DailyTask dailyTask in tasks)
            {
                if (!dailyTask.PrizeClaimed)
                {
                    TaskView taskView = Instantiate(tasksViewPrefab, tasksContainer).GetComponent<TaskView>();
                    taskView.Construct(dailyTask, _saveLoadService);
                }
            }
        }
    }
}
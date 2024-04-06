using System;
using DailyTasks;
using Infrastructure.Services.SaveLoad;
using Infrastructure.States;
using Items.Task;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Ui.Windows.TasksWindow
{
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI prizeCount;
        [SerializeField] private Image taskIcon;

        [Space(10)]
        [SerializeField] private Button claimPrize;
        [SerializeField] private Button claimAdsPrize;

        [Space(10)] 
        [SerializeField] private Image progressbar;
        [SerializeField] private TextMeshProUGUI progressCounter;
        
        private DailyTask _dailyTask;
        private ISaveLoadService _saveLoadService;
        private IGameStateMachine _stateMachine;
        private DailyTasksWindow _dailyTasksWindow;
        
        public void Construct(DailyTask dailyTask, ISaveLoadService saveLoadService, IGameStateMachine stateMachine, DailyTasksWindow dailyTasksWindow)
        {
            _dailyTask = dailyTask;
            _saveLoadService = saveLoadService;
            _stateMachine = stateMachine;
            _dailyTasksWindow = dailyTasksWindow;

            DailyTaskItem taskItem = _dailyTask.TaskItem;
            
            description.text = taskItem.Description.Get();
            prizeCount.text = taskItem.PrizeCount.ToString();
            taskIcon.sprite = taskItem.Icon;

            int targetValue = taskItem.TargetValue;
            int currentCount = dailyTask.CurrentCount;
            progressbar.fillAmount = (float) currentCount / targetValue;
            progressCounter.text = $"{Math.Clamp(currentCount, 0, targetValue)}/{targetValue}";

            bool completed = dailyTask.Completed;
            claimPrize.interactable = completed;
            claimAdsPrize.interactable = completed;
        }

        public void OnClaimPrizeButtonPressed()
        {
            DisableButtons();

            YandexGame.savesData.Gems += _dailyTask.TaskItem.PrizeCount;
            _dailyTask.PrizeClaimed = true;
            
            _saveLoadService.SaveGameState();
            
            Destroy(gameObject);
        }
        
        public void OnClaimAdsPrizeButtonPressed()
        {
            DisableButtons();
            _dailyTasksWindow.Close();
            
            _stateMachine.Enter<ShowDailyTasksAdsState, DailyTask>(_dailyTask);
        }

        private void DisableButtons()
        {
            claimPrize.interactable = false;
            claimAdsPrize.interactable = false;
        }
    }
}
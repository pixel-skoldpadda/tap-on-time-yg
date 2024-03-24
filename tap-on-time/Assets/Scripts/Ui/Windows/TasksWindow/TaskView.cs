using System;
using DailyTasks;
using Infrastructure.Services.SaveLoad;
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
        
        public void Construct(DailyTask dailyTask, ISaveLoadService saveLoadService)
        {
            _dailyTask = dailyTask;
            _saveLoadService = saveLoadService;

            DailyTaskItem taskItem = _dailyTask.TaskItem;
            //: TODO Реализовать локализацию дескрипшенов таксков
            description.text = taskItem.Description;
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
            
            //: TODO Реализовать выдачу двойной награды за рекламу
        }

        private void DisableButtons()
        {
            claimPrize.interactable = false;
            claimAdsPrize.interactable = false;
        }
    }
}
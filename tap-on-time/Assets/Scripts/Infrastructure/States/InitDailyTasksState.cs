using System;
using System.Collections.Generic;
using Configs;
using DailyTasks;
using Data;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Items;
using Infrastructure.States.Interfaces;
using Items.Task;
using UnityEngine;
using YG;

namespace Infrastructure.States
{
    public class InitDailyTasksState : IState
    {
        private const long OneDayInSeconds = 86400;
        
        private readonly IGameStateMachine _stateMachine;
        private readonly IItemsService _items;
        private readonly IDailyTasksFactory _tasksFactory;

        public InitDailyTasksState(IGameStateMachine stateMachine, IItemsService items, IDailyTasksFactory tasksFactory)
        {
            _stateMachine = stateMachine;
            _items = items;
            _tasksFactory = tasksFactory;
        }
        
        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");

            SavesYG saveData = YandexGame.savesData;
            
            long lastTasksUpdateTime = saveData.LastTasksUpdateTime;
            if (lastTasksUpdateTime > 0)
            {
                long currentTime = DateTime.UtcNow.UnixTimeStamp();
                if (currentTime - lastTasksUpdateTime >= OneDayInSeconds)
                {
                    saveData.LastTasksUpdateTime = currentTime;
                    saveData.Tasks.Clear();
                    CreateNewTasks();
                }
                else
                {
                    InitTasks();
                }
            }
            else
            {
                saveData.LastTasksUpdateTime = DateTime.UtcNow.UnixTimeStamp();
                CreateNewTasks();
            }
            
            _stateMachine.Enter<LoadLevelState, string>(SceneConfig.GameScene);
        }
        
        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }

        private void CreateNewTasks()
        {
            List<DailyTask> tasks = YandexGame.savesData.Tasks;
            foreach (DailyTaskItem taskItem in _items.DailyTaskItems)
            {
                tasks.Add(_tasksFactory.CreateDailyTask(taskItem));
            }
        }

        private void InitTasks()
        {
            List<DailyTask> tasks = YandexGame.savesData.Tasks;
            foreach (DailyTask task in tasks)
            {
                task.TaskItem = _items.GetDailyTaskItemById(task.ID);
                if (!task.Completed)
                {
                    task.InitProgressListener();   
                }
            }
        }
    }
}
using System.Collections.Generic;
using Configs;
using DailyTasks;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Items;
using Infrastructure.States.Interfaces;
using Items.Task;
using ModestTree;
using UnityEngine;
using YG;

namespace Infrastructure.States
{
    public class InitDailyTasksState : IState
    {
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
            
            // TODO Проверять время прошедшее с последней инициализации тасков/обновлять раз в сутки
            List<DailyTask> tasks = YandexGame.savesData.Tasks;
            if (tasks.IsEmpty())
            {
                foreach (DailyTaskItem taskItem in _items.DailyTaskItems)
                {
                    tasks.Add(_tasksFactory.CreateDailyTask(taskItem));
                }
            }
            else
            {
                foreach (DailyTask task in tasks)
                {
                    task.TaskItem = _items.GetDailyTaskItemById(task.ID);
                    if (!task.Completed)
                    {
                        task.InitProgressListener();   
                    }
                }
            }
            _stateMachine.Enter<LoadLevelState, string>(SceneConfig.GameScene);
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}
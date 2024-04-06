using DailyTasks;
using Infrastructure.Ads;
using Infrastructure.States.Interfaces;
using UnityEngine;
using YG;

namespace Infrastructure.States
{
    public class ShowDailyTasksAdsState : IPayloadedState<DailyTask>
    {
        private readonly GameStateMachine _stateMachine;
        private DailyTask _dailyTask;

        public ShowDailyTasksAdsState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(DailyTask dailyTask)
        {
            Debug.Log($"{GetType()} entered.");
            
            _dailyTask = dailyTask;

            YandexGame.RewardVideoEvent += OnRewardAdsShowSuccessful;
            YandexGame.ErrorVideoEvent += OnRewardAdsError;
            
            YandexGame.RewVideoShow((int) AdsRewardType.DoubleReward);
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
            
            YandexGame.RewardVideoEvent -= OnRewardAdsShowSuccessful;
            YandexGame.ErrorVideoEvent -= OnRewardAdsError;
        }

        private void OnRewardAdsError()
        {
            _stateMachine.Enter<WaitInputState>();
        }

        private void OnRewardAdsShowSuccessful(int id)
        {
            if ((int) AdsRewardType.DoubleReward == id)
            {
                YandexGame.savesData.Gems += _dailyTask.TaskItem.PrizeCount * 2;
                _dailyTask.PrizeClaimed = true;
                
                _stateMachine.Enter<WaitInputState>();
            }
        }
    }
}
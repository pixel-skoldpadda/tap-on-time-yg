using Infrastructure.Ads;
using Infrastructure.States.Interfaces;
using UnityEngine;
using YG;
using Zenject;

namespace Infrastructure.States
{
    public class ShowRewardAdsState : IState
    {
        private readonly GameStateMachine stateMachine;
        private readonly DiContainer container;

        public ShowRewardAdsState(GameStateMachine stateMachine, DiContainer container)
        {
            this.stateMachine = stateMachine;
            this.container = container;
        }

        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");

            YandexGame.RewardVideoEvent += OnRewardAdsShowSuccessful;
        }

        private void OnRewardAdsShowSuccessful(int id)
        {
            if ((int) AdsRewardType.ExtraLife == id)
            {
                
            }
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}
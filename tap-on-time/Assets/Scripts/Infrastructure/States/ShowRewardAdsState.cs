using Components.Player;
using Infrastructure.Ads;
using Infrastructure.States.Interfaces;
using UI.Hud;
using UnityEngine;
using YG;
using Zenject;

namespace Infrastructure.States
{
    public class ShowRewardAdsState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DiContainer _container;

        public ShowRewardAdsState(GameStateMachine stateMachine, DiContainer container)
        {
            _stateMachine = stateMachine;
            _container = container;
        }

        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");

            YandexGame.RewardVideoEvent += OnRewardAdsShowSuccessful;
            YandexGame.ErrorVideoEvent += OnRewardAdsError;
            
            YandexGame.RewVideoShow((int) AdsRewardType.ExtraLife);
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
            
            YandexGame.RewardVideoEvent -= OnRewardAdsShowSuccessful;
            YandexGame.ErrorVideoEvent -= OnRewardAdsError;
        }

        private void OnRewardAdsError()
        {
            HideAdsContainer();
            _stateMachine.Enter<RestartLevelState>();
        }

        private void OnRewardAdsShowSuccessful(int id)
        {
            if ((int) AdsRewardType.ExtraLife == id)
            {
                YandexGame.savesData.CurrentLevel.IsAdsRewardShown = true;
                
                HideAdsContainer();
                _container.Resolve<PlayerComponent>().StartMoving();
                _stateMachine.Enter<WaitInputState>();
            }
        }

        private void HideAdsContainer()
        {
            _container.Resolve<Hud>().AdsContainer.Hide();
        }
    }
}
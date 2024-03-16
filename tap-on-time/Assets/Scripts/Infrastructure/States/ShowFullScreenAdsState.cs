using System.Collections;
using Components.Player;
using Infrastructure.States.Interfaces;
using UnityEngine;
using YG;
using Zenject;

namespace Infrastructure.States
{
    public class ShowFullScreenAdsState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly DiContainer _container;

        public ShowFullScreenAdsState(IGameStateMachine stateMachine, DiContainer container)
        {
            _stateMachine = stateMachine;
            _container = container;
        }
        
        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");

            YandexGame.CloseFullAdEvent += OnFullScreenAdClosed;
            YandexGame.ErrorFullAdEvent += OnFullScreenAdError;
            
            _container.Resolve<PlayerComponent>().StartCoroutine(ShowFullScreenAd());
        }

        private IEnumerator ShowFullScreenAd()
        {
            yield return new WaitForSeconds(0.5f);
            YandexGame.FullscreenShow();
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
            
            YandexGame.CloseFullAdEvent -= OnFullScreenAdClosed;
            YandexGame.ErrorFullAdEvent -= OnFullScreenAdError;
        }

        private void OnFullScreenAdError()
        {
            _stateMachine.Enter<StartLevelState>();
        }

        private void OnFullScreenAdClosed()
        {
            _stateMachine.Enter<StartLevelState>();
        }
    }
}
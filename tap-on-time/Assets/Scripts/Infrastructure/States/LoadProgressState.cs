using Configs;
using Infrastructure.States.Interfaces;
using UnityEngine;
using YG;


namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        
        public LoadProgressState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");

            YandexGame.GetDataEvent += OnYandexGameDataLoaded;
        }

        private void OnYandexGameDataLoaded()
        {
            _stateMachine.Enter<LoadLevelState, string>(SceneConfig.GameScene);
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}
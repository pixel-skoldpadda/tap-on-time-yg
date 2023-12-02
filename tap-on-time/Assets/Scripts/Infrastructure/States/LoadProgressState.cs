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
            ChangePlayerLanguage();
            _stateMachine.Enter<LoadLevelState, string>(SceneConfig.GameScene);
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }

        private static void ChangePlayerLanguage()
        {
            string language = YandexGame.EnvironmentData.language;
            YandexGame.savesData.language = language switch
            {
                "ru" => "ru",
                "en" => "en",
                _ => "en"
            };
        }
    }
}
using Components.Player;
using Infrastructure.States.Interfaces;
using UnityEngine;
using YG;
using Zenject;

namespace Infrastructure.States
{
    public class StartLevelState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DiContainer _container;

        private PlayerComponent _playerComponent;
        private LevelGenerator _levelGenerator;
        
        public StartLevelState(GameStateMachine stateMachine, DiContainer container)
        {
            _stateMachine = stateMachine;
            _container = container;
        }

        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");
            
            _container.Resolve<PlayerComponent>().ResetComponent();
            _container.Resolve<LevelGenerator>().GenerateLevel();            
            
            SavesYG data = YandexGame.savesData;

            data.Level++;
            data.LevelStarted = true;
            
            _stateMachine.Enter<WaitInputState>();
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}
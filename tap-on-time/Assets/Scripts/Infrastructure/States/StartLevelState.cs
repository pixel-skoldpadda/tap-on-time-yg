using Components.Player;
using Generator;
using Infrastructure.States.Interfaces;
using UI.Hud;
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

            SavesYG data = YandexGame.savesData;
            data.Level++;
            data.Score = 0;
            data.LevelStarted = true;
            
            PlayerComponent player = _container.Resolve<PlayerComponent>();
            player.ResetComponent();
            player.StartMoving();

            _container.Resolve<LevelGenerator>().ChooseNextLevel();

            Hud hud = _container.Resolve<Hud>();
            hud.PlayModeContainer.Show();
            hud.ProgressContainer.Show();
            
            _stateMachine.Enter<SaveProgressState>();
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}
using Components.Player;
using Infrastructure.States.Interfaces;
using UI.Hud;
using UnityEngine;
using YG;
using Zenject;

namespace Infrastructure.States
{
    public class RestartLevelState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly DiContainer _container;
        
        public RestartLevelState(IGameStateMachine stateMachine, DiContainer container)
        {
            _stateMachine = stateMachine;
            _container = container;
        }
        
        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");
            
            // TODO Show splash
            
            SavesYG data = YandexGame.savesData;

            data.LevelStarted = false;
            _container.Resolve<PlayerComponent>().ResetComponent();
            _container.Resolve<LevelGenerator>().Reset();

            Hud hud = _container.Resolve<Hud>();
            hud.PlayModeContainer.Hide();
            hud.ProgressContainer.Hide();
            
            data.Level--;
            
            _stateMachine.Enter<WaitInputState>();
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}
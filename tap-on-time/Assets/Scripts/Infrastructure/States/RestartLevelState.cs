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
            
            Hud hud = _container.Resolve<Hud>();
            SplashScreen splashScreen = hud.SplashScreen;
            splashScreen.OnSplashHide += _stateMachine.Enter<WaitInputState>;
            splashScreen.Show();
            
            SavesYG data = YandexGame.savesData;

            data.LevelStarted = false;
            _container.Resolve<PlayerComponent>().ResetComponent();
            _container.Resolve<LevelGenerator>().Reset();

            hud.PlayModeContainer.Hide();
            hud.ProgressContainer.Hide();
            
            data.Level--;
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}
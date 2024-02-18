using Components.Player;
using Generator;
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
            
            YandexGame.savesData.LevelStarted = false;
            YandexGame.savesData.CurrentLevel.Reset();
            
            PlayerComponent player = _container.Resolve<PlayerComponent>();
            player.ResetComponent();
            player.StartMoving();
            
            _container.Resolve<LevelGenerator>().Reset();

            hud.PlayModeContainer.Hide();
            hud.ProgressContainer.Hide();
            hud.TapToPLay.SetActive(true);
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}
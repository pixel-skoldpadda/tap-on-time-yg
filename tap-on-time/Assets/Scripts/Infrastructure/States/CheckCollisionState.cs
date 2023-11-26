using Components.Player;
using Infrastructure.States.Interfaces;
using UnityEngine;
using YG;
using Zenject;

namespace Infrastructure.States
{
    public class CheckCollisionState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        
        private readonly DiContainer _container;
        
        public CheckCollisionState(IGameStateMachine stateMachine, DiContainer container)
        {
            _stateMachine = stateMachine;
            _container = container;
        }
        
        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");
            
            PlayerComponent player = _container.Resolve<PlayerComponent>();
            LevelGenerator levelGenerator = _container.Resolve<LevelGenerator>();

            if (player.Collision)
            {
                levelGenerator.GenerateNextSector();
                YandexGame.savesData.Score++;
                _stateMachine.Enter<WaitInputState>();
            }
            else
            {
                _stateMachine.Enter<RestartLevelState>();
            }
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}
using Components;
using Components.Player;
using Generator;
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
            _container.Resolve<TapArea>().Show(player.gameObject.transform.position);
            
            SavesYG state = YandexGame.savesData;

            Sector collidedSector = player.CollidedSector;
            if (collidedSector != null)
            {
                collidedSector.Tap();
                state.Score++;
                player.CollidedSector = null;
                
                if (state.Score >= state.CurrentLevel.TargetScore)
                {
                    _stateMachine.Enter<FinishLevelState>();
                }
                else
                {
                    _container.Resolve<LevelGenerator>().NextLevelStep();
                    _stateMachine.Enter<WaitInputState>();   
                }
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
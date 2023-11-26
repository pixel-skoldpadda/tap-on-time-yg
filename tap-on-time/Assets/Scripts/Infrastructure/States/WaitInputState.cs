using Infrastructure.Services.Input;
using Infrastructure.States.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using YG;

namespace Infrastructure.States
{
    public class WaitInputState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IInputService _input;
        
        public WaitInputState(IGameStateMachine stateMachine, IInputService input)
        {
            _stateMachine = stateMachine;
            _input = input;
        }
        
        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");
            
            _input.Tap().performed += OnTap;
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
            
            _input.Tap().performed -= OnTap;
        }

        private void OnTap(InputAction.CallbackContext context)
        {
            if (YandexGame.savesData.LevelStarted)
            {
                _stateMachine.Enter<CheckCollisionState>();
            }
            else
            {
                _stateMachine.Enter<StartLevelState>();
            }
        }
    }
}
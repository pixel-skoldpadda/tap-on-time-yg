using Infrastructure.Services.SaveLoad;
using Infrastructure.States.Interfaces;
using UnityEngine;

namespace Infrastructure.States
{
    public class SaveProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISaveLoadService _saveLoadService;

        public SaveProgressState(IGameStateMachine stateMachine, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
        }
        
        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");
            
            _saveLoadService.SaveGameState();
            _stateMachine.Enter<WaitInputState>();
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}
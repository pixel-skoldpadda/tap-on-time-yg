using System;
using System.Collections.Generic;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Input;
using Infrastructure.Services.Items;
using Infrastructure.Services.Loader;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.WindowsManager;
using Infrastructure.States.Interfaces;
using Zenject;

namespace Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        [Inject]
        public GameStateMachine(DiContainer diContainer, ISceneLoader sceneLoader, ISaveLoadService saveLoadService, IItemsService items, 
            IGameFactory gameFactory, IInputService input, IUiFactory uiFactory, IWindowsManager windows)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, items),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, gameFactory, uiFactory),
                [typeof(LoadProgressState)] = new LoadProgressState(this),
                [typeof(WaitInputState)] = new WaitInputState(this, input, diContainer),
                [typeof(StartLevelState)] = new StartLevelState(this, diContainer),
                [typeof(CheckCollisionState)] = new CheckCollisionState(this, diContainer),
                [typeof(RestartLevelState)] = new RestartLevelState(this, diContainer),
                [typeof(FinishLevelState)] = new FinishLevelState(this, diContainer),
                [typeof(SaveProgressState)] = new SaveProgressState(this, saveLoadService),
                [typeof(ShowRewardAdsState)] = new ShowRewardAdsState(this, diContainer)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            IPayloadedState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }
    
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}
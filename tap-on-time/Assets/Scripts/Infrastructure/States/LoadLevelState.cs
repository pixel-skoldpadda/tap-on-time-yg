using Infrastructure.Services.Factory;
using Infrastructure.Services.Loader;
using Infrastructure.States.Interfaces;
using UnityEngine;
using YG;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUiFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory, IUiFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            Debug.Log($"{GetType()} entered. Scene name: {sceneName}");
            
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }

        private void OnLoaded()
        {
            InitGameWorld();
            YandexGame.GameReadyAPI();
            _stateMachine.Enter<WaitInputState>();
        }

        private void InitGameWorld()
        {
            _gameFactory.CreateConfetti();
            _gameFactory.CreateTapArea();
            _gameFactory.CreateGameField();
            _gameFactory.CreatePlayer();
            _gameFactory.CreateSectors();
            _gameFactory.CreateLevelGenerator();
            _uiFactory.CreateHud();
            _gameFactory.CreateGems();
        }
    }
}
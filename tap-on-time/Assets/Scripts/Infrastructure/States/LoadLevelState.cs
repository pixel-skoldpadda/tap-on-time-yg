using Infrastructure.Services.Factory;
using Infrastructure.Services.Loader;
using Infrastructure.States.Interfaces;
using Ui.Curtain;
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
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, 
            IUiFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            Debug.Log($"{GetType()} entered. Scene name: {sceneName}");
            
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded, _loadingCurtain.UpdateProgress);
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
            
            _loadingCurtain.Hide(YandexGame.GameReadyAPI);
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _stateMachine.Enter<WaitInputState>();
        }

        private void InitGameWorld()
        {
            _gameFactory.CreateGameField();
            _gameFactory.CreatePlayer();
            _gameFactory.CreateSectors();
            _gameFactory.CreateLevelGenerator();
            _uiFactory.CreateHud();
            _gameFactory.CreateGems();
        }
    }
}
using Components;
using Components.Player;
using Configs;
using Generator;
using Infrastructure.States.Interfaces;
using YG;
using Zenject;

namespace Infrastructure.States
{
    public class FinishLevelState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly DiContainer _diContainer;

        public FinishLevelState(IGameStateMachine stateMachine, DiContainer diContainer)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
        }
        
        public void Enter()
        {
            PlayerComponent player = _diContainer.Resolve<PlayerComponent>();
            LevelGenerator generator = _diContainer.Resolve<LevelGenerator>();
            
            _diContainer.Resolve<Confetti>().Play();
            
            player.ResetComponent();
            generator.Reset();
            generator.ShowGems();

            player.AddMove360EndAction(OnMoveEnd);
            player.StartMove360();
        }

        private void OnMoveEnd()
        {
            SavesYG state = YandexGame.savesData;
            Level currentLevel = state.CurrentLevel;
            currentLevel.Completed = true;
            state.TotalScore += state.Score;
            state.Level++;

            YandexGame.NewLeaderboardScores(GameConfig.LeaderboardId, state.TotalScore);
            
            _stateMachine.Enter<ShowFullScreenAdsState>();
        }

        public void Exit()
        {
            _diContainer.Resolve<PlayerComponent>().RemoveMove360EndAction(OnMoveEnd);
        }
    }
}
using TMPro;
using UnityEngine;

namespace UI.Hud
{
    public class PlayModeContainer : MonoBehaviour
    {
        /**
         * Текстовый компонент для отображения текущего прогресса.
         */
        [SerializeField] private TextMeshProUGUI scoreCounter;
        [SerializeField] private TextMeshProUGUI targetScoreCounter;
        
        /**
         * Иконка старта игры.
         */
        [SerializeField] private GameObject playIcon;

        /**
         * Ui-контейнер прогресса уровня.
         */
        [SerializeField] private GameObject scoreContainer;
        //
        // private IGameStateService _gameStateService;
        //
        // public void Construct(IGameStateService gameStateService)
        // {
        //     _gameStateService = gameStateService;
        //     GameState gameState = _gameStateService.State;
        //     gameState.ScoreChaged += UpdateScore;
        //     gameState.TargetScoreChaged += UpdateTargerScore;
        // }
        //
        // private void Start()
        // {
        //     UpdateScore();
        // }
        //
        // private void OnDestroy()
        // {
        //     _gameStateService.State.ScoreChaged -= UpdateScore;
        // }
        //
        // public void Show()
        // {
        //     playIcon.SetActive(false);
        //     scoreContainer.SetActive(true);
        // }
        //
        // public void Hide()
        // {
        //     playIcon.SetActive(true);
        //     scoreContainer.SetActive(false);
        // }
        //
        // private void UpdateScore()
        // {
        //     scoreCounter.text =_gameStateService.State.Score.ToString();
        // }
        //
        // private void UpdateTargerScore()
        // {
        //     targetScoreCounter.text = _gameStateService.State.TargetScore.ToString();
        // }
    }
}
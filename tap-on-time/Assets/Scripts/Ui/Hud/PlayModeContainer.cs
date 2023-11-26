using TMPro;
using UnityEngine;
using YG;

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
        
        private void Awake()
        {
            SavesYG state = YandexGame.savesData;
            state.ScoreChanged += UpdateScore;
            state.TargetScoreChanged += UpdateTargetScore;
            UpdateScore();
        }

        public void Show()
        {
            playIcon.SetActive(false);
            scoreContainer.SetActive(true);
        }
        
        public void Hide()
        {
            playIcon.SetActive(true);
            scoreContainer.SetActive(false);
        }

        private void OnDestroy()
        {
            SavesYG state = YandexGame.savesData;
            state.ScoreChanged -= UpdateScore;
            state.TargetScoreChanged -= UpdateTargetScore;
        }

        private void UpdateScore()
        {
            scoreCounter.text = YandexGame.savesData.Score.ToString();
        }
        
        private void UpdateTargetScore()
        {
            targetScoreCounter.text = YandexGame.savesData.TargetScore.ToString();
        }
    }
}
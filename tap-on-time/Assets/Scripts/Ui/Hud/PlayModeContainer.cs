using DG.Tweening;
using Generator;
using TMPro;
using UnityEngine;
using YG;

namespace UI.Hud
{
    public class PlayModeContainer : BaseHudContainer
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

        private Sequence scaleSequence;
        
        private void Awake()
        {
            SavesYG state = YandexGame.savesData;
            state.ScoreChanged += UpdateScore;
            state.LevelChanged += UpdateTargetScore;
            UpdateScore();
            UpdateTargetScore(state.CurrentLevel);
        }

        public override void Show() 
        {
            playIcon.SetActive(false);
            scoreContainer.SetActive(true);
        }
        
        public override void Hide()
        {
            playIcon.SetActive(true);
            scoreContainer.SetActive(false);
        }

        private void OnDestroy()
        {
            SavesYG state = YandexGame.savesData;
            state.ScoreChanged -= UpdateScore;
            state.LevelChanged -= UpdateTargetScore;
            scaleSequence?.Kill();
        }

        private void UpdateScore()
        {
            if (YandexGame.savesData.Score == 0)
            {
                // TODO Возможно стоит разрулить иначе
                UpdateScoreCallback();
            }
            else
            {
                scaleSequence?.Kill();
                Transform scoreTransform = scoreCounter.transform;
                scaleSequence = DOTween.Sequence()
                    .Append(scoreTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .07f))
                    .AppendCallback(UpdateScoreCallback)
                    .Append(scoreTransform.DOScale(Vector3.one, .07f));   
            }
        }

        private void UpdateScoreCallback()
        {
            scoreCounter.text = YandexGame.savesData.Score.ToString();
        }

        private void UpdateTargetScore(Level level)
        {
            targetScoreCounter.text = level.TargetScore.ToString();
        }
    }
}
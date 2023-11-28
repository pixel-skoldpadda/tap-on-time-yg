using DG.Tweening;
using TMPro;
using UnityEngine;
using YG;

namespace UI.Hud
{
    public class ScoreContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI score;
        
        private Sequence scaleSequence;
        
        private void Awake()
        {
            YandexGame.savesData.TotalScoreChanged += TotalScoreChanged;
            YandexGame.SwitchLangEvent += OnLanguageChanged;
        }

        private void Start()
        {
            UpdateTotalScore(YandexGame.savesData.TotalScore);
        }

        private void OnDestroy()
        {
            YandexGame.savesData.TotalScoreChanged -= TotalScoreChanged;
            YandexGame.SwitchLangEvent -= OnLanguageChanged;
        }

        private void OnLanguageChanged(string lang)
        {
            UpdateTotalScore(YandexGame.savesData.TotalScore);
        }

        private void TotalScoreChanged(int totalScore)
        {
            scaleSequence?.Kill();
            Transform scoreTransform = score.transform;
            scaleSequence = DOTween.Sequence()
                .Append(scoreTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .07f))
                .AppendCallback(()=> UpdateTotalScore(totalScore))
                .Append(scoreTransform.DOScale(Vector3.one, .07f));   
        }

        private void UpdateTotalScore(int totalScore)
        {
            score.text = $"{score.text.Split(" ")[0]} {totalScore}";
        }
    }
}
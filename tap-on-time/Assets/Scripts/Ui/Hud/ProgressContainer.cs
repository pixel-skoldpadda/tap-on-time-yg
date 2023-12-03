using TMPro;
using UI.Element;
using UnityEngine;
using YG;

namespace UI.Hud
{
    public class ProgressContainer : BaseHudContainer
    {
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private TextMeshProUGUI currentLevel;
        [SerializeField] private TextMeshProUGUI nextLevel;

        private void Start()
        {
            YandexGame.savesData.ScoreChanged += UpdateProgressBar;
            YandexGame.SwitchLangEvent += OnLanguageChanged;
        }

        private void OnDestroy()
        {
            YandexGame.SwitchLangEvent -= OnLanguageChanged;
        }

        private void OnLanguageChanged(string lang)
        {
            ChangeLevels();
        }

        private void UpdateProgressBar()
        {
            SavesYG state = YandexGame.savesData;
            float fillAmount = state.Score / (float) state.CurrentLevel.TargetScore;
            progressBar.UpdateProgress(fillAmount);
        }
        
        public override void Show()
        {
            base.Show();
            
            ChangeLevels();
        }

        private void ChangeLevels()
        {
            int level = YandexGame.savesData.Level;
            currentLevel.text = $"{currentLevel.text.Split(" ")[0]} {level}";
            nextLevel.text = $"{nextLevel.text.Split(" ")[0]} {level + 1}";
        }
    }
}

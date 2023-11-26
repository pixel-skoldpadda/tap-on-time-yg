using TMPro;
using UI.Element;
using UnityEngine;
using YG;

namespace UI.Hud
{
    public class ProgressContainer : MonoBehaviour
    {
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private TextMeshProUGUI currentLevel;
        [SerializeField] private TextMeshProUGUI nextLevel;
        
        private void Awake()
        {
            YandexGame.savesData.ScoreChanged += UpdateProgressBar;
        }

        private void UpdateProgressBar()
        {
            SavesYG state = YandexGame.savesData;
            float fillAmount = state.Score / (float) state.TargetScore;
            progressBar.UpdateProgress(fillAmount);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            
            int level = YandexGame.savesData.Level;
            currentLevel.text = level.ToString();
            nextLevel.text = $"{level + 1}";
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

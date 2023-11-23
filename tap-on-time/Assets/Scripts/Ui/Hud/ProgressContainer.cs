using TMPro;
using UI.Element;
using UnityEngine;

namespace UI.Hud
{
    public class ProgressContainer : MonoBehaviour
    {
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private TextMeshProUGUI currentLevel;
        [SerializeField] private TextMeshProUGUI nextLevel;
        
        // private GameState _gameState;
        //
        // public void Construct(IGameStateService gameStateService)
        // {
        //     _gameState = gameStateService.State;
        //     _gameState.ScoreChaged += UpdateProgressBar;
        // }
        //
        // private void UpdateProgressBar()
        // {
        //     float fillAmount = _gameState.Score / (float) _gameState.TargetScore;
        //     progressBar.UpdateProgress(fillAmount);
        // }
        //
        // public void Show()
        // {
        //     gameObject.SetActive(true);
        //     
        //     int level = _gameState.Level;
        //     currentLevel.text = level.ToString();
        //     nextLevel.text = $"{level + 1}";
        // }
        //
        // public void Hide()
        // {
        //     gameObject.SetActive(false);
        // }
    }
}

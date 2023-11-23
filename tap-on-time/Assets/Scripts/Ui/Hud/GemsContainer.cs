using UnityEngine;

namespace UI.Hud
{
    /**
     * Ui контейнер на HUD, отображающий количество набранных гемов.
     */
    public class GemsContainer : MonoBehaviour
    {
        // /**
        //  * Текстовый компонент для отображения количества собранных гемов.
        //  */
        // [SerializeField] private TextMeshProUGUI gemsCounter;
        //
        // private IGameStateService _gameStateService;
        //
        // public void Construct(IGameStateService gameStateService)
        // {
        //     _gameStateService = gameStateService;
        //     _gameStateService.State.GemsChaged += UpdateGemsCount;
        // }
        //
        // private void Start()
        // {
        //     UpdateGemsCount();
        // }
        //
        // private void OnDestroy()
        // {
        //     _gameStateService.State.GemsChaged -= UpdateGemsCount;
        // }
        //
        // private void UpdateGemsCount()
        // {
        //     gemsCounter.text = _gameStateService.State.Gems.ToString();
        // }
    }
}
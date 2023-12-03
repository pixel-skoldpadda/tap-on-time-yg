using TMPro;
using UnityEngine;
using YG;

namespace UI.Hud
{
    /**
     * Ui контейнер на HUD, отображающий количество набранных гемов.
     */
    public class GemsContainer : BaseHudContainer
    {
        /**
         * Текстовый компонент для отображения количества собранных гемов.
         */
        [SerializeField] private TextMeshProUGUI gemsCounter;
        
        private void Awake()
        {
            YandexGame.savesData.GemsChanged += UpdateGemsCount;
            UpdateGemsCount();
        }
        
        private void OnDestroy()
        {
            YandexGame.savesData.GemsChanged -= UpdateGemsCount;
        }
        
        private void UpdateGemsCount()
        {
            gemsCounter.text = YandexGame.savesData.Gems.ToString();
        }
    }
}
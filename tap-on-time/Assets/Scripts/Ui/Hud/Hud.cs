using UnityEngine;

namespace UI.Hud
{
    public class Hud : MonoBehaviour
    {
        private PlayModeContainer playModeContainer;

        [SerializeField] private GameObject tapToPLay;
        [SerializeField] private ProgressContainer progressContainer;
        [SerializeField] private SplashScreen splashScreen;
        [SerializeField] private MarketContainer marketContainer;
        [SerializeField] private SettingsContainer settingsContainer;
        [SerializeField] private AdsContainer adsContainer;
        
        public ProgressContainer ProgressContainer => progressContainer;
        public SplashScreen SplashScreen => splashScreen;
        public GameObject TapToPLay => tapToPLay;
        public MarketContainer MarketContainer => marketContainer;
        public SettingsContainer SettingsContainer => settingsContainer;
        public AdsContainer AdsContainer => adsContainer;

        public PlayModeContainer PlayModeContainer
        {
            get => playModeContainer;
            set => playModeContainer = value;
        }
    }
}

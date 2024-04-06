using UnityEngine;

namespace UI.Hud
{
    public class Hud : MonoBehaviour
    {
        private PlayModeContainer _playModeContainer;

        [SerializeField] private GameObject tapToPLay;
        [SerializeField] private ProgressContainer progressContainer;
        [SerializeField] private SplashScreen splashScreen;
        [SerializeField] private MarketContainer marketContainer;
        [SerializeField] private TasksContainer tasksContainer;
        [SerializeField] private SettingsContainer settingsContainer;
        [SerializeField] private AdsContainer adsContainer;
        [SerializeField] private GemsContainer gemsContainer;
        [SerializeField] private ScoreContainer scoreContainer;
        
        public ProgressContainer ProgressContainer => progressContainer;
        public SplashScreen SplashScreen => splashScreen;
        public GameObject TapToPLay => tapToPLay;
        public MarketContainer MarketContainer => marketContainer;
        public TasksContainer TasksContainer => tasksContainer;
        public SettingsContainer SettingsContainer => settingsContainer;
        public AdsContainer AdsContainer => adsContainer;
        public GemsContainer GemsContainer => gemsContainer;
        public ScoreContainer ScoreContainer => scoreContainer;

        public PlayModeContainer PlayModeContainer
        {
            get => _playModeContainer;
            set => _playModeContainer = value;
        }

        public void Hide()
        {
            tapToPLay.SetActive(false);
            marketContainer.Hide();
            tasksContainer.Hide();
            settingsContainer.Hide();
            gemsContainer.Hide();
            scoreContainer.Hide();
        }

        public void Show()
        {
            tapToPLay.SetActive(true);
            marketContainer.Show();
            tasksContainer.Show();
            settingsContainer.Show();
            gemsContainer.Show();
            scoreContainer.Show();
        }
    }
}

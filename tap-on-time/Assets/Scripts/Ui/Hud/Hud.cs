using UnityEngine;

namespace UI.Hud
{
    public class Hud : MonoBehaviour
    {
        private PlayModeContainer playModeContainer;
        
        [SerializeField] private GemsContainer gemsContainer;
        [SerializeField] private ProgressContainer progressContainer;
        [SerializeField] private SplashScreen splashScreen;
        
        public GemsContainer GemsContainer => gemsContainer;
        public ProgressContainer ProgressContainer => progressContainer;
        public SplashScreen SplashScreen => splashScreen;

        public PlayModeContainer PlayModeContainer
        {
            get => playModeContainer;
            set => playModeContainer = value;
        }
    }
}

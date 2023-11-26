using UnityEngine;

namespace UI.Hud
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private GemsContainer gemsContainer;
        [SerializeField] private PlayModeContainer playModeContainer;
        [SerializeField] private ProgressContainer progressContainer;
        [SerializeField] private SplashScreen splashScreen;
        
        public GemsContainer GemsContainer => gemsContainer;
        public PlayModeContainer PlayModeContainer => playModeContainer;
        public ProgressContainer ProgressContainer => progressContainer;
        public SplashScreen SplashScreen => splashScreen;
    }
}

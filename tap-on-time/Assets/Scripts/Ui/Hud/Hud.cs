using UnityEngine;

namespace UI.Hud
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private GemsContainer gemsContainer;
        [SerializeField] private PlayModeContainer playModeContainer;
        [SerializeField] private ProgressContainer progressContainer;
        
        public GemsContainer GemsContainer => gemsContainer;
        public PlayModeContainer PlayModeContainer => playModeContainer;
        public ProgressContainer ProgressContainer => progressContainer;
    }
}

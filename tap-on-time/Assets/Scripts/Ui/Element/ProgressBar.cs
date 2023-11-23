using ModestTree;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progress;

        public void UpdateProgress(float fillAmount)
        {
            progress.fillAmount = fillAmount;
            Log.Debug($"{fillAmount}");
        }

        public float GetProgress()
        {
            return progress.fillAmount;
        }
    }
}
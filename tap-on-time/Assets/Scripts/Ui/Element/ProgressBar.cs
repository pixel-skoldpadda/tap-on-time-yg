using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progress;

        private Tweener fillTween;
        
        public void UpdateProgress(float fillAmount)
        {
            if (fillAmount == 0)
            {
                // TODO Возможно стоит разрулить иначе
                progress.fillAmount = fillAmount;
            }
            else
            {
                fillTween?.Kill();
                fillTween = progress.DOFillAmount(fillAmount, 0.5f)
                    .SetEase(Ease.OutBack)
                    .OnKill(()=> progress.fillAmount = fillAmount);   
            }
        }

        private void OnDestroy()
        {
            fillTween?.Kill();
        }
    }
}
using DG.Tweening;
using UnityEngine;

namespace Components
{
    public class TapArea : MonoBehaviour
    {
        [SerializeField] private Transform spriteTransform;
        
        private Sequence scaleSequence;
        
        public void Show(Vector3 position)
        {
            scaleSequence?.Kill(true);
            gameObject.transform.position = position;
            
            scaleSequence = DOTween.Sequence()
                .Append(spriteTransform.DOScale(1f, .2f))
                .AppendCallback(() => spriteTransform.gameObject.SetActive(false))
                .OnComplete(() =>
                {
                    spriteTransform.localScale = Vector3.zero;
                    spriteTransform.gameObject.SetActive(true);
                });
        }
    }
}
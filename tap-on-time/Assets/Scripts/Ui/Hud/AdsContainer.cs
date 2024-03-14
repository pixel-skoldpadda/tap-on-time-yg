using DG.Tweening;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Hud
{
    public class AdsContainer : BaseHudContainer
    {
        [SerializeField] private float time;
        [SerializeField] private Image fillTimer;
        [SerializeField] private Transform adsIcon;

        private Tweener _fillTween;
        private Sequence _bounceSequence;
        
        private IGameStateMachine _stateMachine;

        public void Construct(DiContainer container)
        {
            _stateMachine = container.Resolve<IGameStateMachine>();
        }

        public override void Show()
        {
            base.Show();

            _fillTween = fillTimer.DOFillAmount(0, time)
                .OnComplete(OnSkipAdsButtonClicked);

            if (_bounceSequence == null)
            {
                _bounceSequence = DOTween.Sequence()
                    .Append(adsIcon.DOScale(new Vector3(1.2f, 1.2f), .35f))
                    .Append(adsIcon.DOScale(Vector3.one, .35f))
                    .SetEase(Ease.InSine)
                    .SetLoops(-1);   
            }
        }

        public override void Hide()
        {
            base.Hide();

            fillTimer.fillAmount = 1;
            KillFillTween();
        }

        public void OnShowAdsButtonClicked()
        {
            _stateMachine.Enter<ShowRewardAdsState>();
        }

        public void OnSkipAdsButtonClicked()
        {
            Hide();
            
            _stateMachine.Enter<RestartLevelState>();   
        }

        private void OnDestroy()
        {
            KillFillTween();
            KillBounceTween();
        }

        private void KillFillTween()
        {
            if (_fillTween != null)
            {
                _fillTween.Kill();
                _fillTween = null;
            }
        }
        
        private void KillBounceTween()
        {
            if (_bounceSequence != null)
            {
                _bounceSequence.Kill();
                _bounceSequence = null;
            }
        }
    }
}
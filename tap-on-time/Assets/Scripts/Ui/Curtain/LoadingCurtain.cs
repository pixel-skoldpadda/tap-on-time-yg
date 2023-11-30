using System;
using DG.Tweening;
using UI.Element;
using UnityEngine;

namespace Ui.Curtain
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup curtain;
        [SerializeField] private ProgressBar progressBar;
        
        private Tween _fadeTwin;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            _fadeTwin?.Kill(true);
            gameObject.SetActive(true);
            curtain.alpha = 1;
        }

        public void Hide(Action onComplete = null)
        {
            _fadeTwin = curtain.DOFade(0f, 1.5f)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    _fadeTwin = null;
                    onComplete?.Invoke();
                })
                .OnKill(() => curtain.alpha = 1f);
        }

        private void OnDestroy()
        {
            if (_fadeTwin != null)
            {
                _fadeTwin.Kill(true);
                _fadeTwin = null;
            }
        }

        public void UpdateProgress(float progress)
        {
            progressBar.UpdateProgress(Mathf.Clamp01(progress / .9f));   
        }
    }
}
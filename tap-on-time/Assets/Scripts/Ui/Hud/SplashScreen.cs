using System;
using System.Collections;
using UnityEngine;

namespace UI.Hud
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup splashBackground;
        
        private Action onSplashHide;
        
        public void Show()
        {
            gameObject.SetActive(true);
            splashBackground.alpha = 1;
            StartCoroutine(DoFadeIn());
        }

        private IEnumerator DoFadeIn()
        {
            while (splashBackground.alpha > 0)
            {
                splashBackground.alpha -= 0.05f;
                yield return new WaitForSeconds(0.03f);
            }
      
            gameObject.SetActive(false);
            onSplashHide?.Invoke();
            onSplashHide = null;
        }
        
        public Action OnSplashHide
        {
            get => onSplashHide;
            set => onSplashHide = value;
        }
    }
}
using System.Collections;
using UI.Element;
using UnityEngine;

namespace UI.Hud
{
    public class AdsContainer : MonoBehaviour
    {
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private int coolDown;
        
        public void Show()
        {
            gameObject.SetActive(true);
            progressBar.UpdateProgress(1f);

            StartCoroutine(UpdateProgressBar());
        }

        private IEnumerator UpdateProgressBar()
        {
            float normalizedTime = 1;
            while (normalizedTime >= 0)
            {
                progressBar.UpdateProgress(normalizedTime);
                normalizedTime -= Time.deltaTime / coolDown;
                yield return null;
            }
            Hide();
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
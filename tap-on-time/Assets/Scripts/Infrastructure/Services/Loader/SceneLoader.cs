using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.Loader
{
    public class SceneLoader : ISceneLoader
    {
        private ICoroutineRunner _coroutineRunner;
        
        public void Init(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null, Action<float> onProgressUpdated = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded, onProgressUpdated));
        }

        public IEnumerator LoadScene(string nextScene, Action onLoaded = null, Action<float> onProgressUpdated = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            while (!waitNextScene.isDone)
            {               
                onProgressUpdated?.Invoke(waitNextScene.progress);
                yield return null;   
            }

            onProgressUpdated?.Invoke(waitNextScene.progress);
            onLoaded?.Invoke();
        }
    }
}
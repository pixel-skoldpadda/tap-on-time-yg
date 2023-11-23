using System;

namespace Infrastructure.Services.Loader
{
    public interface ISceneLoader
    {
        void Init(ICoroutineRunner coroutineRunner);
        void Load(string name, Action onLoaded = null, Action<float> onProgressUpdated = null);
    }
}
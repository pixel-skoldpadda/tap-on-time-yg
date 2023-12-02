using Infrastructure.Services.Assets;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Input;
using Infrastructure.Services.Items;
using Infrastructure.Services.Loader;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.WindowsManager;
using Infrastructure.States;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IAssetsService>().To<AssetsService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IItemsService>().To<ItemsService>().AsSingle();
            Container.Bind<IUiFactory>().To<UiFactory>().AsSingle();
            Container.Bind<IWindowsManager>().To<WindowsManager>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        }
    }
}
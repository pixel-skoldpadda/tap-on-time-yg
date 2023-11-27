using Infrastructure.Services.Assets;
using Infrastructure.Services.Items;
using Infrastructure.Services.WindowsManager;
using Infrastructure.States;
using Items;
using UI.Hud;
using Ui.Windows;
using Zenject;

namespace Infrastructure.Services.Factory
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssetsService _assets;
        private readonly IItemsService _items;
        private readonly DiContainer _diContainer;
        private readonly IGameStateMachine _stateMachine;

        [Inject]
        public UiFactory(IItemsService items, IAssetsService assets, DiContainer diContainer)
        {
            _items = items;
            _assets = assets;
            _diContainer = diContainer;
        }
        
        public void CreateHud()
        {
            Hud hud = _assets.Instantiate(AssetsPath.HudPrefabPath).GetComponent<Hud>();
            hud.PlayModeContainer = _assets.Instantiate(AssetsPath.PlayModeContainer).GetComponent<PlayModeContainer>();
            
            _diContainer.Bind<Hud>().FromInstance(hud).AsSingle();
        }
        
        public T CreateWindow<T>(WindowType type, IWindowsManager windowsManager, object[] args) where T : Window
        {
            WindowItem item = _items.GetWindowItem(type);
            Window window = _diContainer.InstantiatePrefabForComponent<T>(item.Prefab);

            return (T) window;
        }
    }
}
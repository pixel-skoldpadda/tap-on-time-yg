using Infrastructure.Services.Assets;
using Infrastructure.Services.WindowsManager;
using UI.Hud;
using Zenject;

namespace Infrastructure.Services.Factory
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssetsService _assets;
        private readonly IWindowsManager _windows;
        private readonly DiContainer _container;

        [Inject]
        public UiFactory(IAssetsService assets, IWindowsManager windows, DiContainer container)
        {
            _assets = assets;
            _container = container;
            _windows = windows;
        }
        
        public void CreateHud()
        {
            Hud hud = _assets.Instantiate(AssetsPath.HudPrefabPath).GetComponent<Hud>();
            hud.PlayModeContainer = _assets.Instantiate(AssetsPath.PlayModeContainer).GetComponent<PlayModeContainer>();
            hud.MarketContainer.Construct(_windows);
            hud.SettingsContainer.Construct(_windows);
            hud.AdsContainer.Construct(_container);
            hud.TasksContainer.Construct(_windows, _container);
            
            _container.Bind<Hud>().FromInstance(hud).AsSingle();
        }
    }
}
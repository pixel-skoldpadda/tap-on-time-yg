using Infrastructure.Services.Assets;
using UI.Hud;
using Zenject;

namespace Infrastructure.Services.Factory
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssetsService _assets;
        private readonly DiContainer _container;

        [Inject]
        public UiFactory(IAssetsService assets, DiContainer container)
        {
            _assets = assets;
            _container = container;
        }
        
        public void CreateHud()
        {
            Hud hud = _assets.Instantiate(AssetsPath.HudPrefabPath).GetComponent<Hud>();
            hud.PlayModeContainer = _assets.Instantiate(AssetsPath.PlayModeContainer).GetComponent<PlayModeContainer>();
            hud.MarketContainer.Construct(_container);
            
            _container.Bind<Hud>().FromInstance(hud).AsSingle();
        }
    }
}
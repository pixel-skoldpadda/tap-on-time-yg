using Infrastructure.States;
using Ui.Windows;
using Zenject;

namespace UI.Hud
{
    public class MarketContainer : BaseHudContainer
    {
        private DiContainer _container;
        
        // TODO придумать как по другому передать сюда {IGameStateMachine}
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        public void OnButtonCLicked()
        {
            _container.Resolve<IGameStateMachine>().Enter<OpenWindowState, WindowType>(WindowType.Market);
        }
    }
}
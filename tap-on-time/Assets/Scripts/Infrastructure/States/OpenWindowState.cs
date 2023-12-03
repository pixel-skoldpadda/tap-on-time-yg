using Infrastructure.Services.WindowsManager;
using Infrastructure.States.Interfaces;
using UI.Hud;
using Ui.Windows;
using Zenject;

namespace Infrastructure.States
{
    public class OpenWindowState : IPayloadedState<WindowType>
    {
        private readonly IWindowsManager _windows;
        private readonly DiContainer _container;
        private readonly IGameStateMachine _stateMachine;
        
        public OpenWindowState(IGameStateMachine stateMachine, IWindowsManager windows, DiContainer container)
        {
            _windows = windows;
            _container = container;
            _stateMachine = stateMachine;
        }
        
        public void Enter(WindowType payload)
        {
            MarketContainer marketContainer = _container.Resolve<Hud>().MarketContainer;
            marketContainer.Hide();
            
            _windows.OpenWindow(payload, false, () =>
            {
                _stateMachine.Enter<WaitInputState>();
                marketContainer.Show();
            });
        }

        public void Exit()
        {
            
        }
    }
}
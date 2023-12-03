using Infrastructure.Services.WindowsManager;
using Infrastructure.States.Interfaces;
using UI.Hud;
using Ui.Windows;
using UnityEngine;
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
            Hud hud = _container.Resolve<Hud>();
            MarketContainer marketContainer = hud.MarketContainer;
            GameObject tapToPLay = hud.TapToPLay;
            
            marketContainer.Hide();
            tapToPLay.SetActive(false);
                
            _windows.OpenWindow(payload, false, () =>
            {
                _stateMachine.Enter<WaitInputState>();
                marketContainer.Show();
                tapToPLay.SetActive(true);
            });
        }

        public void Exit()
        {
            
        }
    }
}
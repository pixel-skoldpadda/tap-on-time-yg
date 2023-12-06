using Infrastructure.Services.WindowsManager;
using Ui.Windows;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UI.Hud
{
    public class MarketContainer : BaseHudContainer
    {
        [SerializeField] private Button _marketButton;
        private IWindowsManager _windows;
        
        public void Construct(IWindowsManager windows)
        {
            _windows = windows;
        }

        public void OnButtonCLicked()
        {
            _marketButton.interactable = false;
                
            _windows.OpenWindow(WindowType.Market, false, () =>
            {
                _marketButton.interactable = true;
            });
        }
    }
}
using System;
using System.Collections.Generic;
using Infrastructure.Services.Items;
using Items;
using Ui.Windows;
using Zenject;

namespace Infrastructure.Services.WindowsManager
{
    public class WindowsManager : IWindowsManager
    {
        private readonly Queue<WindowData> _queue = new();
        private Window _current;

        private Action _onWindowOpened;
        private Action _onWindowClosed;

        private IItemsService _items;
        private DiContainer _container;
        
        [Inject]
        public void Construct(DiContainer container, IItemsService items)
        {
            _container = container;
            _items = items;
        }
        
        public void CloseWindow(Window window)
        {
            window.OnWindowClosed += () =>
            {
                _current = null;
                _onWindowClosed?.Invoke();
                TryOpenNextWindow();
            };
            window.Hide();
        }

        public void OpenWindow(WindowType windowType, bool closeCurrent = false, Action onClose = null, params object[] args)
        {
            if (closeCurrent && _current != null)
            {
                _queue.Enqueue(new WindowData(windowType, onClose, args));
                CloseWindow(_current);
                return;
            }

            if (_current == null)
            {
                Window window = CreateWindow<Window>(windowType);
                if (onClose != null)
                {
                    window.OnWindowClosed += onClose;   
                }
                
                _current = window;
                window.Show();
                _onWindowOpened?.Invoke();
            }
            else
            {
                _queue.Enqueue(new WindowData(windowType, onClose, args));   
            }
        }

        private void TryOpenNextWindow()
        {
            if (_queue.TryDequeue(out var windowData))
            {
                OpenWindow(windowData.Type, false, windowData.OnClose, windowData.Args);
            }
        }

        private T CreateWindow<T>(WindowType type) where T : Window
        {
            WindowItem item = _items.GetWindowItem(type);
            Window window = _container.InstantiatePrefabForComponent<T>(item.Prefab);
            
            return (T) window;
        }

        public Action OnWindowOpened
        {
            get => _onWindowOpened;
            set => _onWindowOpened = value;
        }

        public Action OnWindowClosed
        {
            get => _onWindowClosed;
            set => _onWindowClosed = value;
        }
    }
}
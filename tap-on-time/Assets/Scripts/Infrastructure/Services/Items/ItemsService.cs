using System.Collections.Generic;
using System.Linq;
using Items;
using Ui.Windows;
using UnityEngine;

namespace Infrastructure.Services.Items
{
    public class ItemsService : IItemsService
    {
        private Dictionary<WindowType, WindowItem> _windows;
        private List<LevelVariantItem> _variantItems;

        public void LoadAllItems()
        {
            LoadWindowItems();
            LoadLevelVariantItems();
        }

        private void LoadLevelVariantItems()
        {
            _variantItems = Resources.LoadAll<LevelVariantItem>(ItemsPath.LevelVariantsPath).ToList();
        }

        private void LoadWindowItems()
        {
            _windows = Resources.LoadAll<WindowItem>(ItemsPath.WindowItemsPath).ToDictionary(
                k => k.Type, v => v);
        }

        public WindowItem GetWindowItem(WindowType type)
        {
            return _windows.TryGetValue(type, out var item) ? item : null;
        }

        public List<LevelVariantItem> VariantItems => _variantItems;
    }
}
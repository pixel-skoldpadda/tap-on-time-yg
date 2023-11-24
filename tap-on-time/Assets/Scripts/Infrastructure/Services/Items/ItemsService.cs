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
        private Dictionary<SkinType, SkinItem> _skins;
        
        private List<LevelVariantItem> _variantItems;
        private PlayerItem _playerItem;
        private SectorsItem _sectorsItem;
        
        public void LoadAllItems()
        {
            LoadWindowItems();
            LoadLevelVariantItems();
            LoadPlayerItem();
            LoadSkinsItems();
            LoadSectorsItem();
        }

        private void LoadSectorsItem()
        {
            _sectorsItem = Resources.Load<SectorsItem>(ItemsPath.SectorsItemPath);
        }

        private void LoadSkinsItems()
        {
            _skins = Resources.LoadAll<SkinItem>(ItemsPath.SkinsItemsPath).ToDictionary(
                k => k.Type, v => v);
        }

        private void LoadPlayerItem()
        {
            _playerItem = Resources.Load<PlayerItem>(ItemsPath.PlayerItemPath);
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

        public SkinItem GetSkinItem(SkinType type)
        {
            return _skins.TryGetValue(type, out SkinItem item) ? item : null;
        }

        public List<LevelVariantItem> VariantItems => _variantItems;
        public PlayerItem PlayerItem => _playerItem;
        public SectorsItem SectorsItem => _sectorsItem;
    }
}
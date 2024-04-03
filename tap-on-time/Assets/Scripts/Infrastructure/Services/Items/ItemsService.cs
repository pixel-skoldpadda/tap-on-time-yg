using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using Items.Sector;
using Items.Task;
using Ui.Windows;
using UnityEngine;

namespace Infrastructure.Services.Items
{
    public class ItemsService : IItemsService
    {
        private Dictionary<WindowType, WindowItem> _windows;
        private Dictionary<SkinType, SkinItem> _skinItems;
        private Dictionary<SectorType, List<SectorItem>> _sectorItemsGroup;
        private DailyTaskItem[] _dailyTaskItems;
        
        private List<LevelItem> _generatedGeneratedLevelItems;
        private List<LevelItem> _predefinedLevelItems;

        private PlayerItem _playerItem;
        private GemsItem _gemsItem;
        private GameConfig _gameConfig;
        
        public void LoadAllItems()
        {
            LoadWindowItems();
            LoadGeneratedLevelItems();
            LoadPredefinedLevelItems();
            LoadPlayerItem();
            LoadSkinsItems();
            LoadSectorItems();
            LoadGemsItem();
            LoadDailyTasksItems();
            LoadGameConfig();
        }

        private void LoadGameConfig()
        {
            _gameConfig = Resources.Load<GameConfig>(ItemsPath.GameConfigPath);
        }

        private void LoadSectorItems()
        {
            _sectorItemsGroup = new Dictionary<SectorType, List<SectorItem>>();

            List<SectorItem> items = Resources.LoadAll<SectorItem>(ItemsPath.SectorItemsPath).ToList();
            foreach (SectorItem sectorsItem in items)
            {
                SectorType type = sectorsItem.Type;
                if (_sectorItemsGroup.ContainsKey(type))
                {
                    _sectorItemsGroup[type].Add(sectorsItem);
                }
                else
                {
                    _sectorItemsGroup[type] =  new List<SectorItem> { sectorsItem };
                }
            }
        }

        private void LoadPredefinedLevelItems()
        {
            _predefinedLevelItems = Resources.LoadAll<LevelItem>(ItemsPath.PredefinedLevelItemsPath).ToList();
        }

        private void LoadGemsItem()
        {
            _gemsItem = Resources.Load<GemsItem>(ItemsPath.GemsItemPath);
        }
        
        private void LoadSkinsItems()
        {
            SkinItem[] skinItems = Resources.LoadAll<SkinItem>(ItemsPath.SkinsItemsPath);
            Array.Sort(skinItems);

            _skinItems = skinItems.ToDictionary(
                k => k.Type, v => v);
        }

        private void LoadPlayerItem()
        {
            _playerItem = Resources.Load<PlayerItem>(ItemsPath.PlayerItemPath);
        }

        private void LoadGeneratedLevelItems()
        {
            _generatedGeneratedLevelItems = Resources.LoadAll<LevelItem>(ItemsPath.GeneratedLevelItemsPath).ToList();
        }

        private void LoadWindowItems()
        {
            _windows = Resources.LoadAll<WindowItem>(ItemsPath.WindowItemsPath).ToDictionary(
                k => k.Type, v => v);
        }

        private void LoadDailyTasksItems()
        {
            _dailyTaskItems = Resources.LoadAll<DailyTaskItem>(ItemsPath.DailyTasksItemsPath);
            Array.Sort(_dailyTaskItems);
        }
        
        public WindowItem GetWindowItem(WindowType type)
        {
            return _windows.TryGetValue(type, out var item) ? item : null;
        }

        public SkinItem GetSkinItem(SkinType type)
        {
            return _skinItems.TryGetValue(type, out SkinItem item) ? item : null;
        }

        public List<SectorItem> GetSectorItems(SectorType type)
        {
            return _sectorItemsGroup.TryGetValue(type, out List<SectorItem> items) ? items : null;
        }

        public DailyTaskItem GetDailyTaskItemById(string id)
        {
            foreach (DailyTaskItem dailyTaskItem in _dailyTaskItems)
            {
                if (id.Equals(dailyTaskItem.ID))
                {
                    return dailyTaskItem;
                }
            }
            return null;
        }
        
        public List<LevelItem> GeneratedLevelItems => _generatedGeneratedLevelItems;
        public List<LevelItem> PredefinedLevelItems => _predefinedLevelItems;

        public PlayerItem PlayerItem => _playerItem;
        public GemsItem GemsItem => _gemsItem;
        public List<SkinItem> SkinItems => _skinItems.Values.ToList();
        public DailyTaskItem[] DailyTaskItems => _dailyTaskItems;
        public GameConfig GameConfig => _gameConfig;
    }
}
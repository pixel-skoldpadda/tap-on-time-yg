using System.Collections.Generic;
using Infrastructure.Services.Items;
using Items;
using Items.Sector;
using ModestTree;
using UnityEngine;
using Random = System.Random;

namespace Generator
{
    public class Level
    {
        private readonly LevelItem _levelItem;
        private readonly IItemsService _items;
        private readonly List<SectorItem> _sectorsQueue = new();
        
        private bool _completed;
        private int _targetScore;
        private int _currentIndex;
        
        private bool _adsRewardShown;
        
        public Level(LevelItem levelItem, IItemsService items)
        {
            _levelItem = levelItem;
            _items = items;

            Init();
        }
        
        private void Init()
        {
            List<SectorType> sectorTypes = _levelItem.SectorTypes;
            List<SectorItem> sectorItemsPool = new List<SectorItem>();
            
            foreach (SectorType type in sectorTypes)
            {
                sectorItemsPool.AddRange(_items.GetSectorItems(type));
            }

            Random random = new Random();
            int sectorsAmount = _levelItem.SectorsAmount;

            for (int i = 0; i < sectorsAmount; i++)
            {
                if (sectorItemsPool.IsEmpty())
                {
                    sectorItemsPool.AddRange(_sectorsQueue);
                }

                SectorItem sectorItem;
                if (i == sectorsAmount - 1)
                {
                    _sectorsQueue.Add(sectorItem = _items.GetSectorItems(SectorType.Finish)[0]);
                }
                else
                {
                    int index = random.Next(0, sectorItemsPool.Count);
                    _sectorsQueue.Add(sectorItem = sectorItemsPool[index]);
                    sectorItemsPool.RemoveAt(index);
                }

                _targetScore += sectorItem.Health;
            }
        }

        public SectorItem GetNextSectorItem()
        {
            SectorItem sectorItem = _sectorsQueue[_currentIndex];
            _currentIndex++;
            return sectorItem;
        }

        public bool CanCreateTwoSectors()
        {
            return _sectorsQueue.Count - _currentIndex >= 3;
        }

        public void Reset()
        {
            _completed = false;
            _currentIndex = 0;
            _adsRewardShown = false;
        }
        
        public bool Completed
        {
            get => _completed;
            set => _completed = value;
        }

        public int TargetScore => _targetScore;
        public int ChangeDirection => _levelItem.ChangeDirectionProbability;
        public int MoveSector => _levelItem.MoveSectorsProbability;
        public int PlayerSpeed => _levelItem.StartSpeed;
        public int GenerateTwoSectorsProbability => _levelItem.GenerateTwoSectorsProbability;

        public bool IsAdsRewardShown
        {
            get => _adsRewardShown;
            set => _adsRewardShown = value;
        }

        public Color BackgroundColor => _levelItem.BackgroundColor;
        public Color FieldColor => _levelItem.FieldColor;
    }
}
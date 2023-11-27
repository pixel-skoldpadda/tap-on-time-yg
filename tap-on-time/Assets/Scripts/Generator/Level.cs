using Items;
using UnityEngine;

namespace Generator
{
    public class Level
    {
        private readonly LevelItem _levelItem;
        private readonly int _index;
        private bool _completed;

        public Level(LevelItem levelItem, int index)
        {
            _levelItem = levelItem;
            _index = index;
        }
        
        public int Index => _index;

        public bool Completed
        {
            get => _completed;
            set => _completed = value;
        }

        public int TargetScore => _levelItem.Points;
        public int ChangeDirection => _levelItem.ChangeDirectionProbability;
        public int MoveSector => _levelItem.MoveSectorsProbability;
        public int PlayerSpeed => _levelItem.StartSpeed;

        public Color BackgroundColor => _levelItem.BackgroundColor;
        public Color FieldColor => _levelItem.FieldColor;
    }
}

using System;
using Generator;
using Items;
using UnityEngine;

namespace YG
{
    [Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        [SerializeField] private int totalScore;
        [SerializeField] private int gems;
        [SerializeField] private int level;
        [SerializeField] private int levelIndex;
        [SerializeField] private SkinType skinType = SkinType.Watermelon;

        private Level _currentLevel;
        private bool _levelStarted;
        private int score;

        private Action _gemsChanged;
        private Action _scoreChanged;
        private Action<int> _totalScoreChanged;
        private Action<Level> _levelChanged;

        public SkinType SkinType
        {
            get => skinType;
            set => skinType = value;
        }

        public int Gems
        {
            get => gems;
            set
            {
                gems = value;
                _gemsChanged?.Invoke();
            }
        }

        public int Level
        {
            get => level;
            set => level = value;
        }

        public int Score
        {
            get => score;
            set
            {
                score = value;
                _scoreChanged?.Invoke();
            }
        }
        
        public Action GemsChanged
        {
            get => _gemsChanged;
            set => _gemsChanged = value;
        }
        
        public Action ScoreChanged
        {
            get => _scoreChanged;
            set => _scoreChanged = value;
        }

        public Action<Level> LevelChanged
        {
            get => _levelChanged;
            set => _levelChanged = value;
        }

        public bool LevelStarted
        {
            get => _levelStarted;
            set => _levelStarted = value;
        }

        public int LevelIndex
        {
            get => levelIndex;
            set => levelIndex = value;
        }

        public Level CurrentLevel
        {
            get => _currentLevel;
            set
            {
                _currentLevel = value;
                _levelChanged?.Invoke(_currentLevel);
            }
        }

        public int TotalScore
        {
            get => totalScore;
            set
            {
                totalScore = value;
                _totalScoreChanged?.Invoke(totalScore);
            }
        }

        public Action<int> TotalScoreChanged
        {
            get => _totalScoreChanged;
            set => _totalScoreChanged = value;
        }
    }
}

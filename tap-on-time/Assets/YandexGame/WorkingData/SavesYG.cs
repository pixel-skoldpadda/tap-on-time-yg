
using System;
using System.Collections.Generic;
using DailyTasks;
using Generator;
using Items;
using Items.Sector;
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
        [SerializeField] private SkinType skinType = SkinType.Rocket;
        [SerializeField] private List<SkinType> purchasedSkins = new() { SkinType.Rocket };
        [SerializeField] private List<DailyTask> _tasks = new();
        [SerializeField] private long lastTasksUpdateTime;
        
        private Level _currentLevel;
        private bool _levelStarted;
        private int score;

        private Action<int, int> _gemsChanged;
        private Action _scoreChanged;
        private Action<int> _totalScoreChanged;
        private Action<Level> _levelChanged;
        private Action<SkinType> _onSkinChanged;

        private Action<SectorType> _onSectorDestroyed;

        private Action _onTaskCompleted;
        private Action _onTaskPrizeClaimed;
        
        private bool _gamePaused;
        
        public SkinType SkinType
        {
            get => skinType;
            set
            {
                skinType = value;
                _onSkinChanged?.Invoke(skinType);
            }
        }

        public int Gems
        {
            get => gems;
            set
            {
                _gemsChanged?.Invoke(gems, value);
                gems = value;
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
        
        public Action<int, int> GemsChanged
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

        public Action<SkinType> OnSkinChanged
        {
            get => _onSkinChanged;
            set => _onSkinChanged = value;
        }

        public bool GamePaused
        {
            get => _gamePaused;
            set => _gamePaused = value;
        }

        public Action<SectorType> OnSectorDestroyed
        {
            get => _onSectorDestroyed;
            set => _onSectorDestroyed = value;
        }

        public long LastTasksUpdateTime
        {
            get => lastTasksUpdateTime;
            set => lastTasksUpdateTime = value;
        }

        public Action OnTaskCompleted
        {
            get => _onTaskCompleted;
            set => _onTaskCompleted = value;
        }

        public Action OnTaskPrizeClaimed
        {
            get => _onTaskPrizeClaimed;
            set => _onTaskPrizeClaimed = value;
        }

        public List<DailyTask> Tasks => _tasks;
        public List<SkinType> PurchasedSkins => purchasedSkins;
    }
}

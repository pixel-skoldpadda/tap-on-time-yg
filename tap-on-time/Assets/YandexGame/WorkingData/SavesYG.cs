
using System;
using Generator;
using Items;
using UnityEngine;

namespace YG
{
    [Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        
        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        [SerializeField] private int gems;
        [SerializeField] private int level;
        [SerializeField] private int score;
        [SerializeField] private int levelIndex;
        [SerializeField] private SkinType skinType;

        private Level _currentLevel;
        private bool _levelStarted;

        private Action _gemsChanged;
        private Action _scoreChanged;
        private Action<Level> _levelChanged;

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            gems = 0;
            score = 0;
            level = 0;
            levelIndex = 0;
            skinType = SkinType.Rocket;

            _currentLevel = null;
            _levelStarted = false;
            
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
        
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
    }
}

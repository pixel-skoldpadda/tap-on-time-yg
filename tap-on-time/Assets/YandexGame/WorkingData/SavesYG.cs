
using System;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private int targetScore;
        [SerializeField] private SkinType skinType;

        private bool levelStarted;
        
        private Action gemsChanged;
        private Action scoreChanged; 
        private Action targetScoreChanged;

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            gems = 0;
            score = 0;
            level = 0;
            targetScore = 0;
            skinType = SkinType.Rocket;
            
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
                gemsChanged?.Invoke();
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
                scoreChanged?.Invoke();
            }
        }
        
        public int TargetScore
        {
            get => targetScore;
            set
            {
                targetScore = value;
                targetScoreChanged?.Invoke();
            }
        }
        
        public Action GemsChanged
        {
            get => gemsChanged;
            set => gemsChanged = value;
        }
        
        public Action ScoreChanged
        {
            get => scoreChanged;
            set => scoreChanged = value;
        }
        
        public Action TargetScoreChanged
        {
            get => targetScoreChanged;
            set => targetScoreChanged = value;
        }

        public bool LevelStarted
        {
            get => levelStarted;
            set => levelStarted = value;
        }
    }
}

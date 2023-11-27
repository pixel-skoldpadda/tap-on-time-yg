using System.Collections.Generic;
using Components;
using Components.Player;
using DG.Tweening;
using Infrastructure.Services.Items;
using Items;
using ModestTree;
using UnityEngine;
using YG;
using Random = System.Random;

namespace Generator
{
    public class LevelGenerator
    {
        private readonly List<Sector> _allSectors;
        private readonly List<LevelItem> _levelsPool = new();

        /**
     * финишный сектор.
     */
        private readonly Sector _finishSector;

        /**
     * Текущий видимый сектор.
     */
        private Sector _currentSector;

        /**
     * Список, который будет спользоватся для выборки следующего сектора.
     */
        private readonly List<Sector> _sectorsPool = new();

        /**
     * Коорднатные четверти в которых будем генерровать сектора.
     */
        private readonly int[,] _angleRanges = { {0, 90}, {90, 180}, {180, 270}, {270, 360} };

        private int _currentVariantIndex;

        private readonly IItemsService _items;
        private readonly PlayerComponent _player;
        private readonly List<Gem> _gems;

        private readonly SpriteRenderer _gameField;
        private readonly Camera _camera;
    
        public LevelGenerator(List<Sector> sectors, Sector finishSector, List<Gem> gems, IItemsService items, PlayerComponent player, SpriteRenderer gameField)
        {
            _items = items;
            _finishSector = finishSector;
            _gameField = gameField;
            _camera = Camera.main;
            _allSectors = sectors;
            _player = player;
            _gems = gems;

            Init();
        }

        private void Init()
        {
            _levelsPool.AddRange(_items.LevelItems);

            int levelIndex = YandexGame.savesData.LevelIndex;
            YandexGame.savesData.CurrentLevel = new Level(GetLevelFromPool(levelIndex), levelIndex);

            SetupLevel();
        }

        public void ChooseNextLevel()
        {
            if (_levelsPool.IsEmpty())
            {
                _levelsPool.AddRange(_items.LevelItems);
            }

            if (YandexGame.savesData.CurrentLevel.Completed)
            {
                Random random = new Random();
                int levelIndex = random.Next(0, _levelsPool.Count);
                YandexGame.savesData.CurrentLevel = new Level(GetLevelFromPool(levelIndex), levelIndex);   
            }

            _player.ChangeSpeed(YandexGame.savesData.CurrentLevel.PlayerSpeed);
            SetupLevel();
            GenerateNextSector();
        }

        private void SetupLevel()
        {
            SavesYG state = YandexGame.savesData;
            Level currentLevel = state.CurrentLevel;

            _gameField.DOColor(currentLevel.FieldColor, 0.7f);
            _camera.DOColor(currentLevel.BackgroundColor, 0.7f);
            
            state.LevelIndex = currentLevel.Index;
        }
    

        public void GenerateNextSector()
        {
            if (_currentSector != null)
            {
                _currentSector.gameObject.SetActive(false);
                _currentSector.GetComponent<Sector>().Move = false;
                _sectorsPool.Remove(_currentSector);
            }

            if (_sectorsPool.IsEmpty())
            {
                _sectorsPool.AddRange(_allSectors);
            }

            SavesYG state = YandexGame.savesData;
            Level _currentLevel = state.CurrentLevel;

            Random random = new Random();
            if (state.Score == _currentLevel.TargetScore - 1)
            {
                _currentSector = _finishSector;
            }
            else
            {
                int nextIndex = random.Next(_sectorsPool.Count);
                _currentSector = _sectorsPool[nextIndex];
            }

            int rowIndex = random.Next(_angleRanges.GetUpperBound(0) + 1);
            int minAngle = _angleRanges[rowIndex, 0];
            int maxAngle = _angleRanges[rowIndex, 1];

            int changeDirectionProbability = random.Next(1, 10);
            if (_currentLevel.ChangeDirection > changeDirectionProbability)
            {
                _player.ChangeDirection();
            }

            int movingSectorProbability = random.Next(1, 10);
            if (_currentLevel.MoveSector > movingSectorProbability)
            {
                _currentSector.Move = true;
            }
        
            int angle = random.Next(minAngle, maxAngle);
            _currentSector.transform.RotateAround(Vector3.zero, Vector3.back, angle);
            _currentSector.gameObject.SetActive(true);
        }

        public void Reset()
        {
            if (_currentSector != null)
            {
                _currentSector.gameObject.SetActive(false);
                _currentSector = null;
            }
            _sectorsPool.Clear();
            _sectorsPool.AddRange(_allSectors);
        }

        public void ShowGems()
        {
            foreach (Gem gem in _gems)
            {
                gem.Show();
            }
        }

        private LevelItem GetLevelFromPool(int index)
        {
            LevelItem level = _levelsPool[index];
            _levelsPool.RemoveAt(index);
            return level;
        }
    }
}
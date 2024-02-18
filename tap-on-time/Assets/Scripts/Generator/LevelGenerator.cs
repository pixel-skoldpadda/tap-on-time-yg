using System.Collections.Generic;
using Components;
using Components.Player;
using DG.Tweening;
using Infrastructure.Services.Factory;
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
        private readonly List<LevelItem> _levelsPool = new();
        private readonly List<Quarter> _quartersPool = new();
        private readonly List<Sector> _generatedSectors = new();

        private readonly List<Quarter> quarters = new()
        {
            new Quarter(0, 90),
            new Quarter(90, 180),
            new Quarter(180, 270),
            new Quarter(270, 360)
        };

        private readonly IItemsService _items;
        private readonly PlayerComponent _player;
        private readonly List<Gem> _gems;

        private readonly SpriteRenderer _gameField;
        private readonly IGameFactory _factory;
        private readonly Camera _camera;
        
        public LevelGenerator(List<Gem> gems, IItemsService items, PlayerComponent player, SpriteRenderer gameField, IGameFactory factory)
        {
            _items = items;
            _gameField = gameField;
            _factory = factory;
            _camera = Camera.main;
            _player = player;
            _gems = gems;

            Init();
        }

        private void Init()
        {
            SavesYG state = YandexGame.savesData;

            int levelIndex = state.LevelIndex;
            if (IsAllLevelsCompleted())
            {
                _levelsPool.AddRange(_items.GeneratedLevelItems);
                state.CurrentLevel = new Level(GetLevelFromPool(levelIndex), _items);
            }
            else
            {
                state.CurrentLevel = new Level(_items.PredefinedLevelItems[levelIndex], _items);
            }

            SetupLevel();
        }

        public void ChooseNextLevel()
        {
            SavesYG state = YandexGame.savesData;
            if (state.CurrentLevel.Completed)
            {
                if (IsAllLevelsCompleted())
                {
                    InitGeneratedLevel(state);
                }
                else
                {
                    InitPredefineLevel(state);
                }
            }

            _player.ChangeSpeed(state.CurrentLevel.PlayerSpeed);
            
            SetupLevel();
            NextLevelStep();
        }

        public void NextLevelStep()
        {
            Random random = new Random();
            SavesYG state = YandexGame.savesData;
            Level _currentLevel = state.CurrentLevel;

            int changeDirectionProbability = random.Next(1, 10);
            if (_currentLevel.ChangeDirection > changeDirectionProbability)
            {
                _player.ChangeDirection();
            }

            CreateSectors();
        }

        private void InitPredefineLevel(SavesYG state)
        {
            int level = state.Level;
            state.CurrentLevel = new Level(_items.PredefinedLevelItems[level], _items);
            state.LevelIndex = level;
        }

        private void InitGeneratedLevel(SavesYG state)
        {
            if (_levelsPool.IsEmpty())
            {
                _levelsPool.AddRange(_items.GeneratedLevelItems);
            }

            Random random = new Random();
            int levelIndex = random.Next(0, _levelsPool.Count);
            state.CurrentLevel = new Level(GetLevelFromPool(levelIndex), _items);
            state.LevelIndex = levelIndex;
        }

        private void SetupLevel()
        {
            Level currentLevel = YandexGame.savesData.CurrentLevel;

            _gameField.DOColor(currentLevel.FieldColor, 0.7f);
            _camera.DOColor(currentLevel.BackgroundColor, 0.7f);
        }

        private void CreateSectors()
        {
            if (!_generatedSectors.IsEmpty())
            {
                return;
            }

            SavesYG state = YandexGame.savesData;
            Level _currentLevel = state.CurrentLevel;

            Random random = new Random();

            int angle = GetRandomAngle(random);
            bool canMove = CanMove(_currentLevel.MoveSector, random);
            
            int spawnTwoSectors = random.Next(1, 10);
            if (_currentLevel.CanCreateTwoSectors() && _currentLevel.GenerateTwoSectorsProbability > spawnTwoSectors)
            {
                Sector first = _factory.CreateSector(_currentLevel.GetNextSectorItem(), angle, canMove);
                Sector second = _factory.CreateSector(_currentLevel.GetNextSectorItem(), angle + 180, canMove);
                
                _generatedSectors.Add(first);
                _generatedSectors.Add(second);

                first.OnTaped += OnSectorTapped;
                second.OnTaped += OnSectorTapped;
            }
            else
            {
                Sector singleSector = _factory.CreateSector(_currentLevel.GetNextSectorItem(), angle, canMove);
                _generatedSectors.Add(singleSector);
                singleSector.OnTaped += OnSectorTapped;
            }
        }

        private void OnSectorTapped(Sector sector)
        {
            _generatedSectors.Remove(sector);
            sector.OnTaped -= OnSectorTapped;
            Object.Destroy(sector.gameObject);
        }

        private int GetRandomAngle(Random random)
        {
            if (_quartersPool.IsEmpty())
            {
                _quartersPool.AddRange(quarters);
            }

            int quarterIndex = random.Next(_quartersPool.Count);
            Quarter quarter = _quartersPool[quarterIndex];
            _quartersPool.RemoveAt(quarterIndex);
            
            return random.Next(quarter.MinAngle, quarter.MaxAngle);
        }

        private bool CanMove(int moveProbability, Random random)
        {
            int movingSectorProbability = random.Next(1, 10);
            return moveProbability > movingSectorProbability;
        }

        public void Reset()
        {
            foreach (Sector sector in _generatedSectors)
            {
                sector.OnTaped -= OnSectorTapped;
                Object.Destroy(sector.gameObject);
            }
            
            _generatedSectors.Clear();
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

        private bool IsAllLevelsCompleted()
        {
            return YandexGame.savesData.Level > _items.PredefinedLevelItems.Count - 1;
        }
    }
}
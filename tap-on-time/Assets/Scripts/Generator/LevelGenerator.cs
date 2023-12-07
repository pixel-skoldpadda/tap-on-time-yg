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
        private readonly List<LevelItem> _levelsPool = new();
        private readonly List<Quarter> _quartersPool = new();
        private readonly List<Sector> _sectorsPool = new();
        
        private readonly List<Sector> _generatedSectors = new();

        private readonly List<Sector> _allSectors;
        private readonly Sector _finishSector;

        private readonly List<Quarter> quarters = new()
        {
            new Quarter(0, 90),
            new Quarter(90, 180),
            new Quarter(180, 270),
            new Quarter(270, 360)
        };

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
            SavesYG state = YandexGame.savesData;

            int levelIndex = state.LevelIndex;
            if (IsAllLevelsCompleted())
            {
                _levelsPool.AddRange(_items.GeneratedLevelItems);
                state.CurrentLevel = new Level(GetLevelFromPool(levelIndex), levelIndex);
            }
            else
            {
                state.CurrentLevel = new Level(_items.PredefinedLevelItems[levelIndex], levelIndex);
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

            GenerateSectors();
        }

        private void InitPredefineLevel(SavesYG state)
        {
            int level = state.Level;
            state.CurrentLevel = new Level(_items.PredefinedLevelItems[level], level);
        }

        private void InitGeneratedLevel(SavesYG state)
        {
            if (_levelsPool.IsEmpty())
            {
                _levelsPool.AddRange(_items.GeneratedLevelItems);
            }

            Random random = new Random();
            int levelIndex = random.Next(0, _levelsPool.Count);
            state.CurrentLevel = new Level(GetLevelFromPool(levelIndex), levelIndex);
        }

        private void SetupLevel()
        {
            SavesYG state = YandexGame.savesData;
            Level currentLevel = state.CurrentLevel;

            _gameField.DOColor(currentLevel.FieldColor, 0.7f);
            _camera.DOColor(currentLevel.BackgroundColor, 0.7f);

            state.LevelIndex = currentLevel.Index;
        }

        private void GenerateSectors()
        {
            if (!_generatedSectors.IsEmpty())
            {
                return;
            }

            SavesYG state = YandexGame.savesData;
            Level _currentLevel = state.CurrentLevel;
            
            int targetScore = _currentLevel.TargetScore;
            int currentScore = state.Score;
            int moveProbability = _currentLevel.MoveSector;

            Random random = new Random();
            if (currentScore == targetScore - 1)
            {
                _generatedSectors.Add(_finishSector);
                SetupSector(_finishSector, GetRandomAngle(random), CanMove(moveProbability, random));
            }
            else
            {
                int spawnTwoSectors = random.Next(1, 10);
                
                // Проверяем на -3 так как последний сектор всегда должен быть финишным.
                if (_currentLevel.GenerateTwoSectorsProbability > spawnTwoSectors && currentScore <= targetScore - 3)
                {
                    int angle = GetRandomAngle(random);
                    bool canMove = CanMove(moveProbability, random);

                    Sector firstSector = GetSectorFromPool(random);
                    Sector secondSector = GetSectorFromPool(random);

                    SetupSector(firstSector, angle, canMove);
                    SetupSector(secondSector, angle + 180, canMove);

                    _generatedSectors.Add(firstSector);
                    _generatedSectors.Add(secondSector);
                }
                else
                {
                    Sector sector = GetSectorFromPool(random);
                    SetupSector(sector, GetRandomAngle(random), CanMove(moveProbability, random));
                    _generatedSectors.Add(sector);
                }
            }
        }

        private void OnSectorTapped(Sector sector)
        {
            sector.Reset();
            _generatedSectors.Remove(sector);
            sector.OnTaped -= OnSectorTapped;
        }

        private Sector GetSectorFromPool(Random random)
        {
            if (_sectorsPool.IsEmpty())
            {
                _sectorsPool.AddRange(_allSectors);
            }
            
            int nextIndex = random.Next(_sectorsPool.Count);
            Sector sector = _sectorsPool[nextIndex];
            _sectorsPool.RemoveAt(nextIndex);
            return sector;
        }

        private void SetupSector(Sector sector, int angle, bool canMove)
        {
            sector.Move = canMove;
            sector.transform.RotateAround(Vector3.zero, Vector3.back, angle);
            sector.gameObject.SetActive(true);
            sector.OnTaped += OnSectorTapped;
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
                sector.Reset();
                sector.OnTaped -= OnSectorTapped;
            }
            
            _generatedSectors.Clear();
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

        private bool IsAllLevelsCompleted()
        {
            return YandexGame.savesData.Level > _items.PredefinedLevelItems.Count - 1;
        }
    }
}
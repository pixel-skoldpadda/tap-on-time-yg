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

public class LevelGenerator
{
    private readonly List<Sector> _allSectors;
    private readonly List<LevelVariantItem> _levelsPool = new();

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
    private LevelVariantItem _currentLevel;

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
        _levelsPool.AddRange(_items.VariantItems);
        _currentLevel = GetLevelFromPool(YandexGame.savesData.LevelIndex);

        ChangeLevelColor();
    }

    public void ChooseNextLevel()
    {
        if (_levelsPool.IsEmpty())
        {
            _levelsPool.AddRange(_items.VariantItems);
        }
        
        Random random = new Random();
        int levelIndex = random.Next(0, _levelsPool.Count);
        _currentLevel = GetLevelFromPool(levelIndex);
        
        ChangeLevelColor();
        _player.ChangeSpeed(_currentLevel.StartSpeed);
        
        SavesYG state = YandexGame.savesData;
        state.TargetScore = _currentLevel.Points;
        state.LevelIndex = levelIndex;
        
        GenerateNextSector();
    }

    private void ChangeLevelColor()
    {
        _gameField.DOColor(_currentLevel.FieldColor, 0.7f);
        _camera.DOColor(_currentLevel.BackgroundColor, 0.7f);
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

        Random random = new Random();
        if (YandexGame.savesData.Score == _currentLevel.Points - 1)
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
        if (_currentLevel.ChangeDirectionProbability > changeDirectionProbability)
        {
            _player.ChangeDirection();
        }

        int movingSectorProbability = random.Next(1, 10);
        if (_currentLevel.MoveSectorsProbability > movingSectorProbability)
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

    private LevelVariantItem GetLevelFromPool(int index)
    {
        LevelVariantItem level = _levelsPool[index];
        _levelsPool.RemoveAt(index);
        return level;
    }
}
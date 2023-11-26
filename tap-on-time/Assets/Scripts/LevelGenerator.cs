using System.Collections.Generic;
using Components.Player;
using Infrastructure.Services.Items;
using Items;
using UnityEngine;
using YG;
using Random = System.Random;

public class LevelGenerator
{
    /**
     * Список всех вариантов секторов.
     */
    private readonly List<Sector> _sectors = new();
    private readonly Sector _finishSector;

    /**
     * Текущий видимый сектор.
     */
    private Sector _currentSector;

    /**
     * Список, который будет спользоватся для выборки следующего сектора.
     */
    private readonly List<Sector> _generatedSectors = new();
    
    /**
     * Коорднатные четверти в которых будем генерровать сектора.
     */
    private readonly int[,] _angleRanges = { {0, 90}, {90, 180}, {180, 270}, {270, 360} };

    private int _currentVariantIndex;
    private LevelVariantItem _currentVariant;
    
    private readonly IItemsService _itemsService;
    private readonly PlayerComponent _player;

    public LevelGenerator(List<Sector> sectors, IItemsService itemsService, PlayerComponent player)
    {
        _itemsService = itemsService;
        _player = player;
        
        int count = sectors.Count;
        for (var i = 0; i < count; i++)
        {
            Sector sector = sectors[i];
            if (i != count - 1)
            {
                _sectors.Add(sector);
                _generatedSectors.Add(sector);
            }
            else
            {
                _finishSector = sector;
            }
        }   
    }
    
    public void GenerateLevel()
    {
        List<LevelVariantItem> variants = _itemsService.VariantItems;
        if (_currentVariantIndex >= variants.Count)
        {
            _currentVariantIndex = 0;
        }

        _currentVariantIndex++;
        _currentVariant = variants[_currentVariantIndex - 1];
        _player.ChangeSpeed(_currentVariant.StartSpeed);
        YandexGame.savesData.TargetScore = _currentVariant.Points;
        
        GenerateNextSector();
    }
    
    public void GenerateNextSector()
    {
        if (_currentSector != null)
        {
            _currentSector.gameObject.SetActive(false);
            _currentSector.GetComponent<Sector>().Move = false;
            _generatedSectors.Remove(_currentSector);
        }

        if (_generatedSectors.Count <= 0)
        {
            _generatedSectors.AddRange(_sectors);
        }

        Random random = new Random();
        if (YandexGame.savesData.Score == _currentVariant.Points - 1)
        {
            _currentSector = _finishSector;
        }
        else
        {
            int nextIndex = random.Next(_generatedSectors.Count);
            _currentSector = _generatedSectors[nextIndex];
        }

        int rowIndex = random.Next(_angleRanges.GetUpperBound(0) + 1);
        int minAngle = _angleRanges[rowIndex, 0];
        int maxAngle = _angleRanges[rowIndex, 1];

        int changeDirectionProbability = random.Next(1, 10);
        if (_currentVariant.ChangeDirectionProbability > changeDirectionProbability)
        {
            _player.ChangeDirection();
        }

        int movingSectorProbability = random.Next(1, 10);
        if (_currentVariant.MoveSectorsProbability > movingSectorProbability)
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
        _generatedSectors.AddRange(_sectors);
    }
}
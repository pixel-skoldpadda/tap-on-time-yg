using System.Collections.Generic;
using Infrastructure.Services.Items;
using Items;
using UnityEngine;
using YG;
using Random = System.Random;

public class LevelGenerator : MonoBehaviour
{
    /**
     * Список всех вариантов секторов.
     */
    private List<GameObject> _sectors;
    private GameObject _finishSector;

    /**
     * Текущий видимый сектор.
     */
    private GameObject _currentSector;

    /**
     * Список, который будет спользоватся для выборки следующего сектора.
     */
    private readonly List<GameObject> _generatedSectors = new List<GameObject>();

    /**
     * Коорднатные четверти в которых будем генерровать сектора.
     */
    private readonly int[,] _angleRanges = { {0, 90}, {90, 180}, {180, 270}, {270, 360} };

    private int _currentVariantIndex;
    private LevelVariantItem _currentVariant;
    
    private IItemsService _itemsService;

    public void Construct(List<GameObject> sectors, IItemsService itemsService)
    {
        _sectors = sectors;
        _itemsService = itemsService;
        _finishSector = sectors[^1];
    }
    
    private void Start()
    {    
        _generatedSectors.AddRange(_sectors);
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
        YandexGame.savesData.TargetScore = _currentVariant.Points;
        
        GenerateNextSector();
    }
    
    public void GenerateNextSector()
    {
        if (_currentSector != null)
        {
            _currentSector.SetActive(false);
            _currentSector.GetComponent<Sector>().Move = false;
            _generatedSectors.Remove(_currentSector);
        }

        if (_generatedSectors.Count <= 0)
        {
            _generatedSectors.AddRange(_sectors);
        }

        var random = new Random();
        if (YandexGame.savesData.Score == _currentVariant.Points - 1)
        {
            _currentSector = _finishSector;
        }
        else
        {
            var nextIndex = random.Next(_generatedSectors.Count);
            _currentSector = _generatedSectors[nextIndex];
        }

        var rowIndex = random.Next(_angleRanges.GetUpperBound(0) + 1);
        var minAngle = _angleRanges[rowIndex, 0];
        var maxAngle = _angleRanges[rowIndex, 1];

        var changeDirectionProbability = random.Next(1, 10);
        if (_currentVariant.ChangeDirectionProbability > changeDirectionProbability)
        {
            // mediator.ChangeRocketDirection();
        }

        var movingSectorProbability = random.Next(1, 10);
        if (_currentVariant.MoveSectorsProbability > movingSectorProbability)
        {
            _currentSector.GetComponent<Sector>().Move = true;
        }
        
        var angle = random.Next(minAngle, maxAngle);
        _currentSector.transform.RotateAround(Vector3.zero, Vector3.back, angle);
        
        _currentSector.SetActive(true);
    }

    public void Reset()
    {
        if (_currentSector != null)
        {
            _currentSector.SetActive(false);
            _currentSector = null;
        }
        _generatedSectors.AddRange(_sectors);
    }
}
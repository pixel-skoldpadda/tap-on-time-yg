using System.Collections.Generic;
using Components.Player;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Items;
using Items;
using UnityEngine;
using YG;
using Zenject;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IItemsService _items;
        private readonly IAssetsService _assets;

        private readonly List<Sector> _sectors = new();

        [Inject]
        public GameFactory(DiContainer diContainer, IItemsService items, IAssetsService assets)
        {
            _diContainer = diContainer;
            _items = items;
            _assets = assets;
        }

        public void CreatePlayer()
        {
            PlayerItem playerItem = _items.PlayerItem;

            PlayerComponent player = Object.Instantiate(playerItem.Prefab, playerItem.StartPoint, Quaternion.identity).GetComponent<PlayerComponent>();
            player.Construct(playerItem, _items.GetSkinItem(YandexGame.savesData.SkinType));
        }

        public void CreateSectors()
        {
            SectorsItem sectorsItem = _items.SectorsItem;
            Vector3 spawnPoint = sectorsItem.SpawnPoint;
            
            foreach (GameObject prefab in sectorsItem.SectorPrefabs)
            {
                Sector sector = Object.Instantiate(prefab, spawnPoint, Quaternion.identity).GetComponent<Sector>();
                sector.gameObject.SetActive(false);
                _sectors.Add(sector);
            }
        }
        
        public void CreateLevelGenerator()
        {
            LevelGenerator levelGenerator = _assets.Instantiate(AssetsPath.LevelGeneratorPrefabPath).GetComponent<LevelGenerator>();
            levelGenerator.Construct(_sectors, _items);
        }
    }
}
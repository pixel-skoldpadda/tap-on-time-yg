using System.Collections.Generic;
using Components;
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
        private readonly List<Gem> _gems = new();
        
        private PlayerComponent _player;
        private LevelGenerator _levelGenerator;

        [Inject]
        public GameFactory(DiContainer diContainer, IItemsService items)
        {
            _diContainer = diContainer;
            _items = items;
        }

        public void CreatePlayer()
        {
            PlayerItem playerItem = _items.PlayerItem;

            GameObject playerGameObject = Object.Instantiate(playerItem.Prefab, playerItem.StartPoint, Quaternion.identity);

            MoveAroundComponent moveAroundComponent = playerGameObject.GetComponent<MoveAroundComponent>();
            moveAroundComponent.Construct(playerItem);
            moveAroundComponent.StartMove();
            
            _player = playerGameObject.GetComponent<PlayerComponent>();
            _player.Construct(_items.GetSkinItem(YandexGame.savesData.SkinType));

            _diContainer.Bind<PlayerComponent>().FromInstance(_player).AsSingle();
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

        public void CreateGems()
        {
            Vector3 spawnPoint = _items.PlayerItem.StartPoint;

            GemsItem gemsItem = _items.GemsItem;
            int amount = gemsItem.Amount;
            GameObject prefab = gemsItem.Prefab;

            float angle = 360f / amount;

            for (int i = 1; i < amount; i++)
            {
                GameObject gemGameObject = Object.Instantiate(prefab, spawnPoint, Quaternion.identity);
                gemGameObject.transform.RotateAround(Vector3.zero, Vector3.back, i * angle);
                
                Gem gem = gemGameObject.GetComponent<Gem>();
                gem.Construct(gemsItem);
                _gems.Add(gem);
            }
        }
        
        public void CreateLevelGenerator()
        {
            _levelGenerator = new LevelGenerator(_sectors, _gems, _items, _player);

            _diContainer.Bind<LevelGenerator>().FromInstance(_levelGenerator).AsSingle();
        }
    }
}
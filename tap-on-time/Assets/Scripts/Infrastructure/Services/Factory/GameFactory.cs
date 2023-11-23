using Components.Player;
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

        [Inject]
        public GameFactory(DiContainer diContainer, IItemsService items)
        {
            _diContainer = diContainer;
            _items = items;
        }

        public void CreatePlayer()
        {
            PlayerItem playerItem = _items.PlayerItem;

            PlayerComponent player = Object.Instantiate(playerItem.Prefab, playerItem.StartPoint, Quaternion.identity).GetComponent<PlayerComponent>();
            player.Construct(playerItem, _items.GetSkinItem(YandexGame.savesData.SkinType));
        }
    }
}
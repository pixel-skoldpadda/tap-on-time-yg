using Items;
using UnityEngine;
using YG;

namespace Components
{
    public class Gem : MonoBehaviour
    {
        private GemsItem _gemsItem;
        
        public void Construct(GemsItem gemsItem)
        {
            _gemsItem = gemsItem;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Hide();
            YandexGame.savesData.Gems += _gemsItem.Cost;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
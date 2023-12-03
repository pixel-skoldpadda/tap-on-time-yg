using System;
using Items;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Ui.MarketWindow
{
    public class MarketItemView : MonoBehaviour
    {
        [SerializeField] private Color selectedOutlineColor;
        [SerializeField] private Color defaultOutlineColor;
        
        [SerializeField] private GameObject selectedIcon;
        [SerializeField] private GameObject lockIcon;
        [SerializeField] private Image itemIcon;
        
        [SerializeField] private Image outline;

        private SkinItem _skinItem;

        private Action<MarketItemView> _onSelectedListener;
        
        public void Construct(SkinItem skinItem)
        {
            _skinItem = skinItem;
            itemIcon.sprite = _skinItem.Sprite;

            UpdateView();
        }

        public void UpdateView()
        {
            SavesYG state = YandexGame.savesData;
            SkinType skinType = _skinItem.Type;
            
            if (state.SkinType.Equals(skinType))
            {
                ActiveState();
            }
            else if (!state.PurchasedSkins.Contains(skinType))
            {
                NotPurchasedState();
            }
            else
            {
                PurchasedState();
            }
        }

        public void OnSelectButtonPressed()
        {
            _onSelectedListener?.Invoke(this);
        }

        public void ChangeOutlineColor(bool selected)
        {
            outline.color = selected ? selectedOutlineColor : defaultOutlineColor;
        }

        public Action<MarketItemView> OnSelectedListener
        {
            get => _onSelectedListener;
            set => _onSelectedListener = value;
        }

        public SkinItem SkinItem => _skinItem;

        private void ActiveState()
        {
            lockIcon.SetActive(false);
            selectedIcon.SetActive(true);
        }

        private void NotPurchasedState()
        {
            lockIcon.SetActive(true);
            selectedIcon.SetActive(false);
        }

        private void PurchasedState()
        {
            lockIcon.SetActive(false);
            selectedIcon.SetActive(false);
        }
    }
}
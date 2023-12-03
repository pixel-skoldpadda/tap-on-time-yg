using System.Collections.Generic;
using Infrastructure.Services.Items;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.WindowsManager;
using Items;
using TMPro;
using Ui.Windows;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace Ui.MarketWindow
{
    public class MarketWindow : Window
    {
        [SerializeField] private GameObject marketItemViewPrefab;
        [SerializeField] private Button buyItemButton;
        [SerializeField] private Button selectSkinButton;
        [SerializeField] private Transform grid;
        
        [SerializeField] private TextMeshProUGUI price;
        
        private MarketItemView _selectedItem;
        private IItemsService _items;
        private ISaveLoadService _saveLoadService;

        private readonly List<MarketItemView> _marketItemViews = new();
        private MarketItemView _currentSelected;
        
        [Inject]
        private void Construct(IWindowsManager windows, IItemsService items, ISaveLoadService saveLoadService)
        {
            base.Construct(windows);
            
            _items = items;
            _saveLoadService = saveLoadService;

            InitGrid();
        }
        
        public void OnBuyButtonClicked()
        {
            if (_currentSelected == null)
            {
                return;
            }

            SavesYG state = YandexGame.savesData;
            SkinItem skinItem = _currentSelected.SkinItem;
            SkinType skinType = skinItem.Type;

            if (state.PurchasedSkins.Contains(skinType))
            {
                return;
            }

            if (skinItem.Price > state.Gems)
            {
                return;
            }
            
            buyItemButton.gameObject.SetActive(false);
            selectSkinButton.gameObject.SetActive(true);
            
            state.Gems -= skinItem.Price;
            state.PurchasedSkins.Add(skinType);

            _currentSelected.UpdateView();
            
            _saveLoadService.SaveGameState();
        }

        public void OnSelectButtonClicked()
        {
            YandexGame.savesData.SkinType = _currentSelected.SkinItem.Type;
            foreach (MarketItemView marketItemView in _marketItemViews)
            {
                marketItemView.UpdateView();
            }
        }

        private void InitGrid()
        {
            List<SkinItem> skins = _items.SkinItems;
            foreach (SkinItem skinItem in skins)
            {
                MarketItemView marketItemView = Instantiate(marketItemViewPrefab, grid).GetComponent<MarketItemView>();
                marketItemView.Construct(skinItem);
                marketItemView.OnSelectedListener += OnItemSelected;
                
                _marketItemViews.Add(marketItemView);
            }
        }

        private void OnItemSelected(MarketItemView marketItemView)
        {
            if (_currentSelected != null)
            {
                _currentSelected.ChangeOutlineColor(false);
            }
            
            _currentSelected = marketItemView;
            _currentSelected.ChangeOutlineColor(true);
            
            SkinItem skinItem = _currentSelected.SkinItem;

            SavesYG state = YandexGame.savesData;
            SkinType skinType = skinItem.Type;
            
            bool notPurchased = !state.PurchasedSkins.Contains(skinType);
            if (notPurchased)
            {
                buyItemButton.gameObject.SetActive(true);
                selectSkinButton.gameObject.SetActive(false);
                
                int skinPrice = skinItem.Price;

                buyItemButton.interactable = state.Gems >= skinPrice;
                price.text = $"{skinPrice}";
            }
            else
            {
                buyItemButton.gameObject.SetActive(false);
                selectSkinButton.gameObject.SetActive(!state.SkinType.Equals(skinType));
            }
        }
    }
}
using System.Collections.Generic;
using Items;
using Ui.Windows;

namespace Infrastructure.Services.Items
{
    public interface IItemsService
    {
        void LoadAllItems();
        WindowItem GetWindowItem(WindowType type);
        List<LevelItem> LevelItems { get; }
        PlayerItem PlayerItem { get; }
        SectorsItem SectorsItem { get; }
        GemsItem GemsItem { get; }
        SkinItem GetSkinItem(SkinType type);
    }
}
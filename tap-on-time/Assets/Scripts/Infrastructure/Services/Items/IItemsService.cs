using System.Collections.Generic;
using Items;
using Ui.Windows;

namespace Infrastructure.Services.Items
{
    public interface IItemsService
    {
        void LoadAllItems();
        WindowItem GetWindowItem(WindowType type);
        List<LevelItem> GeneratedLevelItems { get; }
        PlayerItem PlayerItem { get; }
        SectorsItem SectorsItem { get; }
        GemsItem GemsItem { get; }
        List<LevelItem> PredefinedLevelItems { get; }
        SkinItem GetSkinItem(SkinType type);
    }
}
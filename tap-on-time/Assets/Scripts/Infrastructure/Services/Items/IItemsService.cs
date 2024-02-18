using System.Collections.Generic;
using Items;
using Items.Sector;
using Ui.Windows;

namespace Infrastructure.Services.Items
{
    public interface IItemsService
    {
        void LoadAllItems();
        WindowItem GetWindowItem(WindowType type);
        List<LevelItem> GeneratedLevelItems { get; }
        PlayerItem PlayerItem { get; }
        GemsItem GemsItem { get; }
        List<LevelItem> PredefinedLevelItems { get; }
        List<SkinItem> SkinItems { get; }
        SkinItem GetSkinItem(SkinType type);
        List<SectorItem> GetSectorItems(SectorType type);
    }
}
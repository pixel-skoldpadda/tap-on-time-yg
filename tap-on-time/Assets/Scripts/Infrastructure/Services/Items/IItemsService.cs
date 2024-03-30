using System.Collections.Generic;
using Items;
using Items.Sector;
using Items.Task;
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
        List<DailyTaskItem> DailyTaskItems { get; }
        GameConfig GameConfig { get; }
        SkinItem GetSkinItem(SkinType type);
        List<SectorItem> GetSectorItems(SectorType type);
        DailyTaskItem GetDailyTaskItemById(string id);
    }
}
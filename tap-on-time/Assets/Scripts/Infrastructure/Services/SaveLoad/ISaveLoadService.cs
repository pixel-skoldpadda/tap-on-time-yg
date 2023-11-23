using YG;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveGameState();
        SavesYG LoadGameState();
    }
}
using Data;
using UnityEngine;
using YG;

namespace Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        public void SaveGameState()
        {
            string json = YandexGame.savesData.ToJson();
            YandexGame.SaveProgress();

            Debug.Log($"Game state saved {json}");
        }

        public SavesYG LoadGameState()
        {
            YandexGame.LoadProgress();
            return YandexGame.savesData;
        }
    }
}
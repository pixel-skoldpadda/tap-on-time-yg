using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "GAME_CONFIG_ITEM", menuName = "Items/Game config")]
    public class GameConfig : ScriptableObject
    {
        [Header("GENERAL SETTINGS")]
        [SerializeField] private string leaderboardId;
        [SerializeField] private long updateDailyTasksTime;
        
        [Space(20)] 
        [Header("FEATURE TOGGLES")]
        [SerializeField ]private string showFullScreenAd;

        public string LeaderboardId => leaderboardId;
        public long UpdateDailyTasksTime => updateDailyTasksTime;
        public string ShowFullScreenAd => showFullScreenAd;
    }
}
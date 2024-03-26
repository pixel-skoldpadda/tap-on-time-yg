using Localization;
using UnityEngine;

namespace Items.Task
{
    [CreateAssetMenu(fileName = "_DAILY_TASK_ITEM", menuName = "Items/Task")]
    public class DailyTaskItem : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private DailyTaskType type;
        [SerializeField] private LocalizationString description;
        [SerializeField] private Sprite icon;
        [SerializeField] private int prizeCount;
        [SerializeField] private int targetValue;

        public string ID => id;
        public DailyTaskType Type => type;
        public LocalizationString Description => description;
        public Sprite Icon => icon;
        public int PrizeCount => prizeCount;
        public int TargetValue => targetValue;
    }
}
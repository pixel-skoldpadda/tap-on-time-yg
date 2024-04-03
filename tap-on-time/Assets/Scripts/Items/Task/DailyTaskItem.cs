using System;
using Localization;
using UnityEngine;

namespace Items.Task
{
    [CreateAssetMenu(fileName = "_DAILY_TASK_ITEM", menuName = "Items/Task")]
    public class DailyTaskItem : ScriptableObject, IComparable
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
        
        public int CompareTo(object obj)
        {
            DailyTaskItem taskItem = (DailyTaskItem) obj;
            int typeCompare = Type.CompareTo(taskItem.Type);
            return typeCompare == 0 ? TargetValue.CompareTo(taskItem.TargetValue) : typeCompare;
        }
    }
}
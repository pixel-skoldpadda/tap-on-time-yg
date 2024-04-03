using System;
using Items.Task;
using UnityEngine;

namespace DailyTasks
{
    [Serializable]
    public class DailyTask
    {
        [NonSerialized] private DailyTaskItem _taskItem;

        [SerializeField] private string _id;
        [SerializeField] private int _currentCount;
        [SerializeField] private bool _completed;
        [SerializeField] private bool _prizeClaimed;

        protected DailyTask(DailyTaskItem taskItem)
        {
            _taskItem = taskItem;
            _id = taskItem.ID;
        }

        public virtual void InitProgressListener() {}
        public virtual void RemoveProgressListener() {}

        protected void IncrementCounter()
        {
            _currentCount++;

            if (_currentCount >= _taskItem.TargetValue)
            {
                _completed = true;
                RemoveProgressListener();
            }
        }

        protected void UpdateCounter(int count)
        {
            _currentCount += count;
            
            if (_currentCount >= _taskItem.TargetValue)
            {
                _completed = true;
                RemoveProgressListener();
            }
        }
        
        public string ID => _id;

        public bool Completed
        {
            get => _completed;
            set => _completed = value;
        }

        public int CurrentCount
        {
            get => _currentCount;
            set => _currentCount = value;
        }

        public DailyTaskItem TaskItem
        {
            get => _taskItem;
            set => _taskItem = value;
        }

        public bool PrizeClaimed
        {
            get => _prizeClaimed;
            set => _prizeClaimed = value;
        }
    }
}
using Items.Task;
using YG;

namespace DailyTasks
{
    public sealed class CollectGemsTask : DailyTask
    {
        public CollectGemsTask(DailyTaskItem taskItem) : base(taskItem)
        {
            InitProgressListener();
        }

        public override void InitProgressListener()
        {
            YandexGame.savesData.GemsChanged += OnGemsChanged;
        }

        public override void RemoveProgressListener()
        {
            YandexGame.savesData.GemsChanged -= OnGemsChanged;
        }

        private void OnGemsChanged(int oldCount, int newCount)
        {
            int diff = newCount - oldCount;
            if (diff > 0)
            {
                UpdateCounter(diff);
            }
        }
    }
}
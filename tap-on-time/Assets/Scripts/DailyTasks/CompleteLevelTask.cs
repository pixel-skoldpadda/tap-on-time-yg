using Items.Sector;
using Items.Task;
using YG;

namespace DailyTasks
{
    public sealed class CompleteLevelTask : DailyTask
    {
        public CompleteLevelTask(DailyTaskItem taskItem) : base(taskItem)
        {
            InitProgressListener();
        }
        
        public override void InitProgressListener()
        {
            YandexGame.savesData.OnSectorDestroyed += OnSectorDestroyed;
        }

        public override void RemoveProgressListener()
        {
            YandexGame.savesData.OnSectorDestroyed -= OnSectorDestroyed;
        }

        private void OnSectorDestroyed(SectorType type)
        {
            if (SectorType.Finish.Equals(type))
            {
                IncrementCounter();
            }
        }
    }
}
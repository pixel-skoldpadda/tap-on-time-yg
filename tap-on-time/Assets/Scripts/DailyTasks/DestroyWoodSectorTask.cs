using Items.Sector;
using Items.Task;
using YG;

namespace DailyTasks
{
    public sealed class DestroyWoodSectorTask : DailyTask
    {
        public DestroyWoodSectorTask(DailyTaskItem taskItem) : base(taskItem)
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
            if (SectorType.Wood.Equals(type))
            {
                IncrementCounter();
            }
        }
    }
}
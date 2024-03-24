using DailyTasks;
using Items.Task;

namespace Infrastructure.Services.Factory
{
    public class DailyTasksFactory : IDailyTasksFactory
    {
        public DailyTask CreateDailyTask(DailyTaskItem taskItem)
        {
            return taskItem.Type switch
            {
                DailyTaskType.DestroySector => new DestroySectorTask(taskItem),
                DailyTaskType.DestroyIceSector => new DestroyIceSectorTask(taskItem),
                DailyTaskType.DestroyWoodSector => new DestroyWoodSectorTask(taskItem),
                DailyTaskType.CompleteLevel => new CompleteLevelTask(taskItem),
                DailyTaskType.CollectGems => new CollectGemsTask(taskItem),
                _ => null
            };
        }
    }
}
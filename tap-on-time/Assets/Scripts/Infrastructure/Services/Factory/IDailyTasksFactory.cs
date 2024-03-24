using DailyTasks;
using Items.Task;

namespace Infrastructure.Services.Factory
{
    public interface IDailyTasksFactory
    {
        DailyTask CreateDailyTask(DailyTaskItem taskItem);
    }
}
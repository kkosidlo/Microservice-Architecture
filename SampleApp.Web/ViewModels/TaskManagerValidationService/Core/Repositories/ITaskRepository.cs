using TaskManagerValidationService.Core.Model;

namespace TaskManagerValidationService.Core.Repositories
{
    public interface ITaskRepository
    {
        bool IsUnique(string taskName);
        void AddTaskToDatabase(Assignment task);
        Assignment GetTaskByname(string taskName);
        void DeleteTask(Assignment taskName);
        void UpdateTask();
    }
}

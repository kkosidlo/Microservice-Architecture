using System;
using System.Linq;
using TaskManagerValidationService.Core.Model;
using TaskManagerValidationService.Core.Repositories;
using TaskManagerValidationService.Dal;

namespace TaskManagerValidationService.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagerValidationServiceContext _taskManagerValidationServiceContext;

        public TaskRepository(
            TaskManagerValidationServiceContext taskManagerValidationServiceContext)
        {
            _taskManagerValidationServiceContext = taskManagerValidationServiceContext 
                ?? throw new ArgumentNullException(nameof(taskManagerValidationServiceContext));
        }

        public void AddTaskToDatabase(Assignment task)
        {
            _taskManagerValidationServiceContext.Assignment.Add(task);
            _taskManagerValidationServiceContext.SaveChanges();
        }

        public void DeleteTask(Assignment task)
        {
            _taskManagerValidationServiceContext
                .Assignment
                .Remove(task);

            _taskManagerValidationServiceContext
                .SaveChanges();
        }
        public void UpdateTask()
        {
            _taskManagerValidationServiceContext.SaveChanges();
        }

        public Assignment GetTaskByname(string taskName)
        {
            return _taskManagerValidationServiceContext
                .Assignment
                .FirstOrDefault(x => x.TaskName.Equals(taskName));
        }

        public bool IsUnique(string taskName)
        {
            return _taskManagerValidationServiceContext
                .Assignment
                .FirstOrDefault(x => x.TaskName.Equals(taskName)) == null;
        }
    }
}

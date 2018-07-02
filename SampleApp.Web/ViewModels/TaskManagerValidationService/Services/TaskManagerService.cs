using System;
using TaskManagerValidationService.Core.Model;
using TaskManagerValidationService.Core.Repositories;
using TaskManagerValidationService.Core.Services;
using TaskManagerValidationService.Core.Validators;
using TaskManagerValidationService.Dtos;
using TaskManagerValidationService.Enums;

namespace TaskManagerValidationService.Services
{
    public class TaskManagerService : ITaskManagerService
    {
        private readonly ITaskNameValidator _taskNameValidator;
        private readonly ITaskRepository _taskRepository;
        public TaskManagerService(ITaskNameValidator taskNameValidator, ITaskRepository taskRepository)
        {
            _taskNameValidator = taskNameValidator ?? throw new ArgumentNullException(nameof(taskNameValidator));
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        public ValidationResultDto ValidateTaskName(string taskName)
        {
            return _taskNameValidator.Validate(taskName);
        }
        public ValidationResultDto ProcessTaskNameCreation(string taskName)
        {
            var validationResult = ValidateTaskName(taskName);

            if (validationResult.IsFailure)
                return validationResult;

            var assignment = new Assignment
            {
                TaskName = taskName,
                
                LastModified = DateTime.Now
            };

            _taskRepository.AddTaskToDatabase(assignment);

            return new ValidationResultDto
            {
                TaskName = taskName,
                IsFailure = false
            };
        }

        public void ProcessTaskNameEdit(EditTaskRequestDto task)
        {
            var taskFromDatabase = _taskRepository.GetTaskByname(task.OldTaskName);

            if (taskFromDatabase != null)
            {
                if (task.TaskStatus == AssignmentStatus.Completed)
                {
                    _taskRepository.DeleteTask(taskFromDatabase);
                }
                else
                {
                    taskFromDatabase.TaskName = task.NewTaskName;
                    taskFromDatabase.LastModified = DateTime.Now;

                    _taskRepository.UpdateTask();
                }
            }
        }
    }
}

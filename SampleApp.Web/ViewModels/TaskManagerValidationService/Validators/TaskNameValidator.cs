using System;
using TaskManagerValidationService.Core.Repositories;
using TaskManagerValidationService.Core.Validators;
using TaskManagerValidationService.Dtos;

namespace TaskManagerValidationService.Validators
{
    public class TaskNameValidator : ITaskNameValidator
    {
        private readonly ITaskRepository _taskRepository;
        public TaskNameValidator(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }
        public ValidationResultDto Validate(string taskName)
        {
            bool isTaskNameProperLength = taskName.Length > 3 
                && taskName.Length <= 50;
            bool isTaskUnique = isTaskNameProperLength 
                && _taskRepository.IsUnique(taskName);

            if (isTaskUnique)
            {
                return ReturnValidationResult(taskName, false);
            }

            return ReturnValidationResult(taskName, true);
        }

        private ValidationResultDto ReturnValidationResult(
            string taskName, bool isFailure)
        {
            return new ValidationResultDto
            {
                TaskName = taskName,
                IsFailure = isFailure
            };
        }
    }
}

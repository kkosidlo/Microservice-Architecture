using TaskManagerValidationService.Dtos;

namespace TaskManagerValidationService.Core.Services
{
    public interface ITaskManagerService
    {
        ValidationResultDto ProcessTaskNameCreation(string taskName);
        ValidationResultDto ValidateTaskName(string taskName);
        void ProcessTaskNameEdit(EditTaskRequestDto task);
    }
}

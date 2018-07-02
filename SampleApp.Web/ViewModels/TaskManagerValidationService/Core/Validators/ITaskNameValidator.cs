using TaskManagerValidationService.Dtos;

namespace TaskManagerValidationService.Core.Validators
{
    public interface ITaskNameValidator
    {
        ValidationResultDto Validate(string taskName);
    }
}

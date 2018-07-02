using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerValidationService.Dtos
{
    public class ValidationResultDto
    {
        public string TaskName { get; set; }
        public bool IsFailure { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerValidationService.Enums;

namespace TaskManagerValidationService.Dtos
{
    public class EditTaskRequestDto
    {
        public string OldTaskName { get; set; }
        public string NewTaskName { get; set; }
        public AssignmentStatus TaskStatus { get; set; }
    }
}

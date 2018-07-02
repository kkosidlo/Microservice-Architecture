using System;

namespace TaskManagerValidationService.Core.Model
{
    public class Assignment
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime LastModified { get; set; }
    }
}

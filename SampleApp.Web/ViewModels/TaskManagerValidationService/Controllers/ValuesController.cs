using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagerValidationService.Core.Services;
using TaskManagerValidationService.Dtos;

namespace TaskManagerValidationService.Controllers
{
    [Route("api")]
    public class ValuesController : Controller
    {
        private readonly ITaskManagerService _taskManagerService;
        public ValuesController(ITaskManagerService taskManagerService)
        {
            _taskManagerService = taskManagerService ?? throw new ArgumentNullException(nameof(taskManagerService));
        }

        [HttpPost("ValidateTaskName")]
        public IActionResult ValidateTaskName([FromBody] ValidateTaskRequestDto task)
        {
            if (task != null && !string.IsNullOrEmpty(task.TaskName))
            {
                var result = _taskManagerService.ProcessTaskNameCreation(task.TaskName);

                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("ValidateAndEditTaskName")]
        public IActionResult ValidateAndEditTaskName([FromBody] EditTaskRequestDto task)
        {
            if (task != null)
            {
                Thread.Sleep(5000);

                bool isSameTaskName = task.OldTaskName.Equals(task.NewTaskName);

                var result = _taskManagerService.ValidateTaskName(task.NewTaskName);

                if (isSameTaskName || !result.IsFailure)
                {
                    _taskManagerService.ProcessTaskNameEdit(task);

                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}

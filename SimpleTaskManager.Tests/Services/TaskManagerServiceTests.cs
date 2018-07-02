using Moq;
using SimpleTaskManager.Core.Services;
using SimpleTaskManager.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTaskManager.Tests.Services
{
    public class TaskManagerServiceTests
    {

        public void CheckTaskNameUniqueness_WhenDuplicatedTaskNameIsPassedThenValidationFailureIsSet()
        {
            var requestServiceMock = Mock.Of<IRequestService>(service => service.SendPostRequest(It.IsAny<string>(), It.IsAny<string>() == new WebResponseDto { }))
        }
    }
}

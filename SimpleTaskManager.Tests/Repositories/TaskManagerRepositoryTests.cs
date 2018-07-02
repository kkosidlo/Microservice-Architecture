using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SimpleTaskManager.Core;
using SimpleTaskManager.Core.Model;
using SimpleTaskManager.Dal;
using SimpleTaskManager.Enums;
using SimpleTaskManager.Repositories;
using SimpleTaskManager.ViewModels;
using System.Collections.Generic;


namespace SimpleTaskManager.Tests.Services
{
    public class TaskManagerRepositoryTests
    {
        private TaskManagerRepository _taskManagerRepository;

        [SetUp]
        public void SetUp()
        {
            CreateDbContext().Database.EnsureDeleted();

            using (var context = CreateDbContext())
            {
                context.Assignment.Add(new Assignment { Id = 1, TaskName = "Test", TaskStatus = (int)AssignmentStatus.Completed });
                context.Assignment.Add(new Assignment { Id = 2, TaskName = "Test1", TaskStatus = (int)AssignmentStatus.Open });
                context.SaveChanges();
            }

            _taskManagerRepository = new TaskManagerRepository(CreateDbContext());
        }

        [Test]
        public void GetTaskById_WhenProperIdIsPassed_ThenTaskWithGivenIdIsReturned()
        {
            var result = _taskManagerRepository.GetTaskById(1);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new Assignment { Id = 1, TaskName = "Test", TaskStatus = (int)AssignmentStatus.Completed });
        }

        [Test]
        public void GetTotalTasksCountByTaskName_WhenProperTaskNameIsPassed_ThenTaskWithGivenIdIsReturned()
        {
            var result = _taskManagerRepository.GetTotalTasksCountByTaskName("Test1");
            result.Should().BeGreaterThan(0);
        }

        [Test]
        public void GetOpenTasksFromDatabase_TasksShouldBeReturnedFromDatabase()
        {
            var result = _taskManagerRepository.GetOpenTasksFromDatabase();

            result.Should().NotBeEmpty();
        }

        private static TaskManagerContext CreateDbContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<TaskManagerContext>()
                .UseInMemoryDatabase("AvivaSectorRepository")
                .Options;
            return new TaskManagerContext(dbContextOptions);
        }

    }
}

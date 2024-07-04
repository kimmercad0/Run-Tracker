using Moq;
using Microsoft.Extensions.Logging;
using RunTracker.API.Services;
using RunTracker.API.Data.Repositories;
using RunTracker.API.Models;

namespace RunTracker.API.Tests.Services
{
    public class RunActivityServiceTests
    {
        [Fact]
        public void AddActivity_ValidActivity_ShouldInsert()
        {
            var actRepositoryMock = new Mock<IRepository<RunActivity>>();
            var loggerMock = new Mock<ILogger<RunActivityService>>();

            var service = new RunActivityService(actRepositoryMock.Object, loggerMock.Object);
            var activity = new RunActivity
            {
                RunId = 1,
                Location = "Place 1",
                DateTimeStarted = DateTime.Now,
                DateTimeEnded = DateTime.Now.AddHours(1),
                Distance = 10 
            };

            // Act
            service.AddActivity(activity);

            // Assert
            actRepositoryMock.Verify(repo => repo.Add(activity), Times.Once);
        }

        [Fact]
        public void DeleteActivity_ExistingActivityId_ShouldDelete()
        {
            var actRepositoryMock = new Mock<IRepository<RunActivity>>();
            var loggerMock = new Mock<ILogger<RunActivityService>>();

            var service = new RunActivityService(actRepositoryMock.Object, loggerMock.Object);
            var activityId = 1;
            var existingActivity = new RunActivity { RunId = activityId };
            actRepositoryMock.Setup(repo => repo.GetById(activityId)).Returns(existingActivity);

            // Act
            service.DeleteActivity(activityId);

            // Assert
            actRepositoryMock.Verify(repo => repo.Delete(existingActivity), Times.Once);
        }

        [Fact]
        public void GetActivity_ExistingActivityId_ShouldReturnActivity()
        {
            var actRepositoryMock = new Mock<IRepository<RunActivity>>();
            var loggerMock = new Mock<ILogger<RunActivityService>>();

            var service = new RunActivityService(actRepositoryMock.Object, loggerMock.Object);
            var activityId = 1;
            var existingActivity = new RunActivity { RunId = activityId };
            actRepositoryMock.Setup(repo => repo.GetById(activityId)).Returns(existingActivity);

            // Act
            var result = service.GetActivity(activityId);

            // Assert
            Assert.Equal(existingActivity, result);
        }

        [Fact]
        public void GetAllActivities_ShouldReturnAllActivities()
        {
            var actRepositoryMock = new Mock<IRepository<RunActivity>>();
            var loggerMock = new Mock<ILogger<RunActivityService>>();

            var service = new RunActivityService(actRepositoryMock.Object, loggerMock.Object);
            var activities = new List<RunActivity>
            {
                new RunActivity { RunId = 1 },
                new RunActivity { RunId = 2 }
            };
            actRepositoryMock.Setup(repo => repo.GetAll()).Returns(activities);

            // Act
            var result = service.GetAllActivities();

            // Assert
            Assert.Equal(activities, result);
        }

        [Fact]
        public void UpdateActivity_ValidActivity_ShouldUpdate()
        {
            var actRepositoryMock = new Mock<IRepository<RunActivity>>();
            var loggerMock = new Mock<ILogger<RunActivityService>>();

            var service = new RunActivityService(actRepositoryMock.Object, loggerMock.Object);
            var activity = new RunActivity
            {
                RunId = 1,
                Location = "Place 1",
                DateTimeStarted = DateTime.Now,
                DateTimeEnded = DateTime.Now.AddHours(1),
                Distance = 10
            };

            // Act
            service.UpdateActivity(activity);

            // Assert
            actRepositoryMock.Verify(repo => repo.Update(activity), Times.Once);
        }
    }
}
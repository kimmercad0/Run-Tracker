using Moq;
using Microsoft.Extensions.Logging;
using RunTracker.API.Models;
using RunTracker.API.Services;
using RunTracker.API.Data.Repositories;

namespace RunTracker.API.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void AddUser_ValidUser_ShouldInsert()
        { 
            var userRepositoryMock = new Mock<IRepository<User>>();
            var loggerMock = new Mock<ILogger<UserService>>();

            var userService = new UserService(userRepositoryMock.Object, loggerMock.Object);
            var user = new User
            {
                Name = "User Name",
                BirthDate = new DateTime(1998, 1, 1),
                Height = 180,
                Weight = 75
            };

            // Act
            userService.AddUser(user);

            // Assert
            userRepositoryMock.Verify(repo => repo.Add(user), Times.Once);
        }

        [Fact]
        public void DeleteUser_ExistingUserId_ShouldDelete()
        {
            var userRepositoryMock = new Mock<IRepository<User>>();
            var loggerMock = new Mock<ILogger<UserService>>();

            var userService = new UserService(userRepositoryMock.Object, loggerMock.Object);
            var userId = 1;
            var existingUser = new User { UserId = userId, Name = "User Name 1" };
            userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns(existingUser);

            // Act
            userService.DeleteUser(userId);

            // Assert
            userRepositoryMock.Verify(repo => repo.Delete(existingUser), Times.Once);
        }

        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            var userRepositoryMock = new Mock<IRepository<User>>();
            var loggerMock = new Mock<ILogger<UserService>>();

            var userService = new UserService(userRepositoryMock.Object, loggerMock.Object);
            var users = new List<User>
            {
                new User { UserId = 1, Name = "User Name 1" },
                new User { UserId = 2, Name = "User Name 2" }
            };
            userRepositoryMock.Setup(repo => repo.GetAll()).Returns(users);

            // Act
            var result = userService.GetAllUsers();

            // Assert
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetUser_ExistingUserId_ShouldReturnUser()
        {
            var userRepositoryMock = new Mock<IRepository<User>>();
            var loggerMock = new Mock<ILogger<UserService>>();

            var userService = new UserService(userRepositoryMock.Object, loggerMock.Object);
            var userId = 1;
            var existingUser = new User { UserId = userId, Name = "User Name 1" };
            userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns(existingUser);

            // Act
            var result = userService.GetUser(userId);

            // Assert
            Assert.Equal(existingUser, result);
        }

        [Fact]
        public void UpdateUser_ValidUser_ShouldUpdate()
        {
            var userRepositoryMock = new Mock<IRepository<User>>();
            var loggerMock = new Mock<ILogger<UserService>>();

            var userService = new UserService(userRepositoryMock.Object, loggerMock.Object);
            var user = new User
            {
                UserId = 1,
                Name = "User Name",
                BirthDate = new DateTime(1998, 1, 1),
                Height = 185,
                Weight = 90
            };

            // Act
            userService.UpdateUser(user);

            // Assert
            userRepositoryMock.Verify(repo => repo.Update(user), Times.Once);
        }
    }
}

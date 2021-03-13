using MailingGroups.Models;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Identity;
using MailingGroups.Controllers;
using MailingGroups.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MailingGroups.API.Controllers;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Infrastructure.Data;
using Core.Entities;

namespace MailingGroups.XUnitTest.API
{
    public class GroupsAPITest
    {
        [Fact]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            var GroupServiceMock = new Mock<IGroupsService>();

            // Act
            var okResult = new GroupController(GroupServiceMock.Object).GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Delete_WhenCalled_ReturnsOkResult(int value)
        {
            var GroupServiceMock = new Mock<IGroupsService>();

            // Act
            var okResult = new GroupController(GroupServiceMock.Object).DeleteGroups(value);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Add_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            GroupType testItem = new GroupType()
            {
                Name = "Test group",
                UserId = "c5e41288-e315-493c-a76e-8e9407b3ca6e",
            };

            var GroupServiceMock = new Mock<IGroupsService>();

            // Act
            var okResult = new GroupController(GroupServiceMock.Object).AddGroup(testItem);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Edit_WhenCalled_ReturnsOkResult(int value)
        {
            // Arrange
            GroupType testItem = new GroupType()
            {
                Name = "Test group",
                UserId = "c5e41288-e315-493c-a76e-8e9407b3ca6e",
                Id = value
            };

            var GroupServiceMock = new Mock<IGroupsService>();

            // Act
            var okResult = new GroupController(GroupServiceMock.Object).EditGroup(testItem);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void GroupById_WhenCalled_ReturnsOkResult(int value)
        {
            var GroupServiceMock = new Mock<IGroupsService>();

            // Act
            var okResult = new GroupController(GroupServiceMock.Object).GetGroupById(value);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
    }
}

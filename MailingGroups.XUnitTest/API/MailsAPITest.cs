using Core.Entities;
using Core.Interfaces;
using MailingGroups.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MailingGroups.XUnitTest.API
{
    public class MailsAPITest
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void GetAllEmails_WhenCalled_ReturnsOkResult(int value)
        {
            var MailServiceMock = new Mock<IMailsService>();
            var GroupServiceMock = new Mock<IGroupsService>();

            // Act
            var okResult = new EmailController(GroupServiceMock.Object, MailServiceMock.Object).GetAllEmails(value);

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
        public void DeleteEmail_WhenCalled_ReturnsOkResult(int value)
        {
            var MailServiceMock = new Mock<IMailsService>();
            var GroupServiceMock = new Mock<IGroupsService>();

            // Act
            var okResult = new EmailController(GroupServiceMock.Object, MailServiceMock.Object).DeleteEmail(value);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void AddEmail_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            MailsType testItem = new MailsType()
            {
                Email = "Test group",
                GroupId = 1,
                GroupModelId = 1
            };

            var MailServiceMock = new Mock<IMailsService>();
            var GroupServiceMock = new Mock<IGroupsService>();

            // Act
            var okResult = new EmailController(GroupServiceMock.Object, MailServiceMock.Object).AddEmail(testItem);

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
        public void EditEmail_WhenCalled_ReturnsOkResult(int value)
        {
            // Arrange
            MailsType testItem = new MailsType()
            {
                Email = "Test group",
                GroupId = 1,
                GroupModelId = 1
            };

            var MailServiceMock = new Mock<IMailsService>();
            var GroupServiceMock = new Mock<IGroupsService>();

            // Act
            var okResult = new EmailController(GroupServiceMock.Object, MailServiceMock.Object).EditEmail(testItem);

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
        public void GetEmailById_WhenCalled_ReturnsOkResult(int value)
        {
            var MailServiceMock = new Mock<IMailsService>();
            var GroupServiceMock = new Mock<IGroupsService>();

            // Act
            var okResult = new EmailController(GroupServiceMock.Object, MailServiceMock.Object).GetEmailById(value);

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
        public void GetGroupIdByEmailId_WhenCalled_ReturnsOkResult(int value)
        {         
            var MailServiceMock = new Mock<IMailsService>();
            var GroupServiceMock = new Mock<IGroupsService>();

            // Act
            var okResult = new EmailController(GroupServiceMock.Object, MailServiceMock.Object).GetGroupIdByEmailId(value);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
    }
}

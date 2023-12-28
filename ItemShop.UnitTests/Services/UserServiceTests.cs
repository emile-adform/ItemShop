using AutoFixture.Xunit2;
using FluentAssertions;
using ItemShop.Clients;
using ItemShop.Exceptions;
using ItemShop.Models.DTOs.UserDtos;
using ItemShop.Models.Entities;
using ItemShop.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemShop.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IJsonPlaceholderClient> _mockClient;
        public UserServiceTests() 
        { 
            _mockClient = new Mock<IJsonPlaceholderClient>();
            _userService = new UserService(_mockClient.Object);
        }

        [Theory]
        [AutoData]
        public async Task GetById_ValidId_ReturnsUserDto(int id, string name)
        {
            var expectedResult = new JsonPlaceholderResult<UserDto>
            {
                Data = new UserDto { Id = id, Name = name },
                IsSuccessful = true,
                ErrorMessage = null
            };
            _mockClient.Setup(c => c.GetUserAsync(id)).ReturnsAsync(expectedResult);

            var user = await _userService.GetById(id);
            user.Id.Should().Be(id);
        }

        [Theory]
        [AutoData]
        public async Task GetById_InValidId_ThrowsUserNotFoundException(int id, string name)
        {
            var expectedResult = new JsonPlaceholderResult<UserDto>
            {
                IsSuccessful = false,
                ErrorMessage = "User not found"
            };
            _mockClient.Setup(c => c.GetUserAsync(id)).ReturnsAsync(expectedResult);

            await _userService.Invoking(async x => await x.GetById(id)).Should().ThrowAsync<UserNotFoundException>();

        }
    }
}

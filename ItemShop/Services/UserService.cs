using ItemShop.Clients;
using ItemShop.Exceptions;
using ItemShop.Models.DTOs.UserDtos;

namespace ItemShop.Services
{
    public class UserService
    {
        private readonly IJsonPlaceholderClient _client;
        public UserService(IJsonPlaceholderClient client)
        {
            _client = client;
        }
        public async Task<UserDto> GetById(int id)
        {
            var result = await _client.GetUserAsync(id);
            if (!result.IsSuccessful)
            {
                throw new UserNotFoundException();
            }
            return result.Data;
        }
        public async Task<List<UserDto>> GetUsers()
        {
            var result = await _client.GetUsersAsync();
            if(!result.IsSuccessful)
            {
                throw new Exception();
            }
            return result.Data;
        }
        public async Task<UserDto> CreateUser(CreateUserDto user)
        {
            var result = await _client.CreateUserAsync(user);
            if (!result.IsSuccessful)
            {
                throw new Exception();
            }
            return result.Data;
        }
    }
}

using ItemShop.Clients;
using ItemShop.Exceptions;
using ItemShop.Models.DTOs.UserDtos;

namespace ItemShop.Services
{
    public class UserService
    {
        private readonly JsonPlaceholderClient _client;
        public UserService(JsonPlaceholderClient client)
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
            return await _client.GetUsers();
        }
        public async Task CreateUser(CreateUserDto user)
        {
            await _client.CreateUser(user);
        }
    }
}

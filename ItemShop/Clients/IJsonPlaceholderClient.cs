using ItemShop.Models.DTOs.UserDtos;

namespace ItemShop.Clients
{
    public interface IJsonPlaceholderClient
    {
        Task<JsonPlaceholderResult<UserDto>> CreateUserAsync(CreateUserDto user);
        Task<JsonPlaceholderResult<UserDto>> GetUserAsync(int userId);
        Task<JsonPlaceholderResult<List<UserDto>>> GetUsersAsync();
    }
}
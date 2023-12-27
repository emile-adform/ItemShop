using ItemShop.Models.DTOs.UserDtos;
using Newtonsoft.Json;
using System.Text;

namespace ItemShop.Clients
{
    public class JsonPlaceholderClient
    {
        private HttpClient _httpClient;
        public JsonPlaceholderClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<UserDto>> GetUsers()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            var users = await response.Content.ReadAsAsync<List<UserDto>>();

            return users;
        }
        public async Task<UserDto> GetUserById(int userId)
        {
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{userId}");
            return await response.Content.ReadAsAsync<UserDto>();
        }
        public async Task<UserDto> CreateUser(CreateUserDto user)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://jsonplaceholder.typicode.com/users", jsonContent);
            var createdUser = await response.Content.ReadAsAsync<UserDto>();
            return createdUser;

        }

    }
}

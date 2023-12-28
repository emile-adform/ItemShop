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
        public async Task<JsonPlaceholderResult<List<UserDto>>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<List<UserDto>>();
                return new JsonPlaceholderResult<List<UserDto>>
                {
                    Data = data,
                    IsSuccessful = true,
                    ErrorMessage = null
                };
            }
            else
            {
                return new JsonPlaceholderResult<List<UserDto>>
                {
                    IsSuccessful = false,
                    ErrorMessage = response.StatusCode.ToString()
                };
            }
        }
        public async Task<JsonPlaceholderResult<UserDto>> GetUserAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<UserDto>();
                return new JsonPlaceholderResult<UserDto>
                {
                    Data = data,
                    IsSuccessful = true,
                    ErrorMessage = null
                };
            }
            else
            {
                return new JsonPlaceholderResult<UserDto>
                {
                    IsSuccessful = false,
                    ErrorMessage = response.StatusCode.ToString()
                };
            }
        }
        public async Task<JsonPlaceholderResult<UserDto>> CreateUserAsync(CreateUserDto user)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://jsonplaceholder.typicode.com/users", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<UserDto>();
                return new JsonPlaceholderResult<UserDto>
                {
                    Data = data,
                    IsSuccessful = true,
                    ErrorMessage = null
                };
            }
            else
            {
                return new JsonPlaceholderResult<UserDto>
                {
                    IsSuccessful = false,
                    ErrorMessage = response.StatusCode.ToString()
                };
            }
            //var createdUser = await response.Content.ReadAsAsync<UserDto>();
            //return createdUser;

        }

    }
}

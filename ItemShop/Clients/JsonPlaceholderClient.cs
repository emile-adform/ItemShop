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
        private async Task<JsonPlaceholderResult<T>> HandleResponse<T>(HttpResponseMessage response, Func<Task<T>> processContent) where T : class
        {
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<T>();
                return new JsonPlaceholderResult<T>
                {
                    Data = data,
                    IsSuccessful = true,
                    ErrorMessage = null
                };
            }
            else
            {
                return new JsonPlaceholderResult<T>
                {
                    IsSuccessful = false,
                    ErrorMessage = response.StatusCode.ToString()
                };
            }
        }
        public async Task<JsonPlaceholderResult<List<UserDto>>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            return await HandleResponse(response, response.Content.ReadAsAsync<List<UserDto>>);
        }
        public async Task<JsonPlaceholderResult<UserDto>> GetUserAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{userId}");
            return await HandleResponse(response, response.Content.ReadAsAsync<UserDto>);
        }
        public async Task<JsonPlaceholderResult<UserDto>> CreateUserAsync(CreateUserDto user)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://jsonplaceholder.typicode.com/users", jsonContent);
            return await HandleResponse(response, response.Content.ReadAsAsync<UserDto>);
        }


    }
}

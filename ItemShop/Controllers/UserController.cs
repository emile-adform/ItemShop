using ItemShop.Clients;
using ItemShop.Models.DTOs.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace ItemShop.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly JsonPlaceholderClient _client;

        public UserController(JsonPlaceholderClient client)
        {
            _client = client;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _client.GetUsers());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _client.GetUserById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserDto user)
        {
            return Ok(await _client.CreateUser(user));
        }

    }
}

using ItemShop.Clients;
using ItemShop.Models.DTOs.UserDtos;
using ItemShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItemShop.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly JsonPlaceholderClient _client;
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetUsers());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _userService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserDto user)
        {
            await _userService.CreateUser(user);
            return Ok();
        }

    }
}

using ItemShop.Models.DTOs.UserDtos;
using ItemShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItemShop.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PurchaseService _purchaseService;

        public UserController(UserService userService, PurchaseService purchaseService)
        {
            _userService = userService;
            _purchaseService = purchaseService;
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
            var createdUser = await _userService.CreateUser(user);
            return CreatedAtAction("Get", new {userId = createdUser.Id}, createdUser);
        }
        [HttpPost("{id}/buy")]
        public async Task<IActionResult> Buy(int id, int itemId)
        {
            await _purchaseService.Create(id, itemId);
            return Ok();
        }

    }
}

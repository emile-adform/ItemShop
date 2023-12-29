using ItemShop.Models.DTOs.ItemDtos;
using ItemShop.Models.DTOs.ShopDtos;
using ItemShop.Models.Entities;
using ItemShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItemShop.Controllers
{
    [ApiController]
    [Route("shop")]
    public class ShopController : ControllerBase
    {
        private readonly ShopService _shopService;
        public ShopController(ShopService shopService)
        {
            _shopService = shopService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _shopService.Get(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _shopService.GetAll());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateShopDto shop)
        {
            await _shopService.Create(shop);
            return Created();
        }
        [HttpPut]
        public async Task<IActionResult> Edit(UpdateShopDto shop)
        {
            await _shopService.Update(shop);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _shopService.Delete(id);
            return NoContent();
        }
    }
}

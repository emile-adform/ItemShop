using ItemShop.Models.DTOs.ItemDtos;
using ItemShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItemShop.Controllers
{
    [ApiController]
    [Route("item")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;
        public ItemController(ItemService itemService)
        {
            _itemService = itemService;  
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _itemService.Get(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _itemService.GetAll());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateItemDto item)
        {
            await _itemService.Create(item);
            return Created();
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateItemDto item)
        {
            await _itemService.Update(item);
            return Ok();
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.Delete(id);
            return NoContent();
        }
        [HttpPut("id")]
        public async Task<IActionResult> AddToShop(int itemId, int shopId)
        {
            await _itemService.AddToShop(itemId, shopId);
            return Ok();
        }

    }
}

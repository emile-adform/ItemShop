using AutoMapper;
using ItemShop.Interfaces;
using ItemShop.Models.DTOs;
using ItemShop.Models.Entities;
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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _itemService.GetItem(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _itemService.GetAllItems());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateItemDto item)
        {
            await _itemService.CreateItem(item);
            return Created();
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateItemDto item)
        {
            await _itemService.UpdateItem(item);
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.DeleteItem(id);
            return NoContent();
        }

    }
}

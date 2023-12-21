using ItemShop.Models.DTOs;
using ItemShop.Models.Entities;

namespace ItemShop.Interfaces
{
    public interface IItemRepository
    {
        public Task Create(Item item);
        public Task Update(Item item);
        public Task<Item> Get(int id);
        public Task<List<Item>> Get();
        public Task Delete(int id);
    }
}

using ItemShop.Models.Entities;

namespace ItemShop.Repositories
{
    public interface IEFItemRepository
    {
        Task Create(Item item);
        Task Delete(Item item);
        Task<List<Item>> Get();
        Task<Item> Get(int Id);
        Task Update(Item item);
    }
}
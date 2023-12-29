using ItemShop.Models.Entities;

namespace ItemShop.Repositories
{
    public interface IShopRepository
    {
        Task Create(Shop shop);
        Task Delete(Shop shop);
        Task<List<Shop>> Get();
        Task<Shop> Get(int Id);
        Task Update(Shop shop);
    }
}
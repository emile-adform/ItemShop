using ItemShop.Contexts;
using ItemShop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemShop.Repositories
{
    public class ShopRepository
    {
        private readonly DataContext _dataContext;
        public ShopRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<Shop>> Get()
        {
            return await _dataContext.Shops.ToListAsync();
        }
        public async Task Create(Shop shop)
        {
            _dataContext.Shops.Add(shop);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<Shop> Get(int Id)
        {
            return await _dataContext.Shops.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task Delete(Shop shop)
        {
            _dataContext.Remove(shop);
            await _dataContext.SaveChangesAsync();
        }
        public async Task Update(Shop shop)
        {
            _dataContext.Update(shop);
            await _dataContext.SaveChangesAsync();
        }
    }
}

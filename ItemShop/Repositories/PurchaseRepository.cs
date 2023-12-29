using ItemShop.Contexts;
using ItemShop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemShop.Repositories
{
    public class PurchaseRepository
    {
        private readonly DataContext _dataContext;
        public PurchaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<Purchase>> Get()
        {
            return await _dataContext.Purchases.ToListAsync();
        }
        public async Task Create(Purchase purchase)
        {
            _dataContext.Purchases.Add(purchase);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<Purchase> Get(int Id)
        {
            return await _dataContext.Purchases.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}

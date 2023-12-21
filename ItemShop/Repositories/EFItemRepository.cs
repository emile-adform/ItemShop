using ItemShop.Contexts;
using ItemShop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemShop.Repositories
{
    public class EFItemRepository
    {
        private readonly DataContext _dataContext;
        public EFItemRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<Item>> Get()
        {
            return await _dataContext.Items.ToListAsync();
        }
        public async Task Create(Item item)
        {
            _dataContext.Items.Add(item);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<Item> Get(int Id)
        {
            return await _dataContext.Items.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task Delete (Item item)
        {
            _dataContext.Remove(item);
            await _dataContext.SaveChangesAsync();
        }
        public async Task Update(Item item)
        {
            _dataContext.Update(item);
            await _dataContext.SaveChangesAsync();
        }

    }
}

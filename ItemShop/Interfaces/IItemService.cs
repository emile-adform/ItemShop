using ItemShop.Models.DTOs;
using ItemShop.Models.Entities;

namespace ItemShop.Interfaces
{
    public interface IItemService
    {
        public Item GetItem(int id);
        public List<Item> GetAllItems();
        public int CreateItem(CreateItemDto item);
        //public int UpdateItem(Item item);
        public int DeleteItem(int id);
    }
}

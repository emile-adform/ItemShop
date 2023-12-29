using AutoMapper;
using ItemShop.Exceptions;
using ItemShop.Interfaces;
using ItemShop.Models.DTOs.ItemDtos;
using ItemShop.Models.Entities;
using ItemShop.Repositories;

namespace ItemShop.Services
{
    public class ItemService
    {

        private readonly IItemRepository _itemRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IShopRepository shopRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _shopRepository = shopRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateItemDto item)
        {
            var entity = _mapper.Map<Item>(item);
            await _itemRepository.Create(entity);
        }

        public async Task Delete(int id)
        {
            var item = await _itemRepository.Get(id);
            if(item == null)
            {
                throw new ItemNotFoundException();
            }
            await _itemRepository.Delete(item);
        }

        public async Task<List<Item>> GetAll()
        {
            List<Item> items = await _itemRepository.Get();
            return items;
        }

        public async Task<Item> Get(int id)
        {
            var item = await _itemRepository.Get(id);
            if (item == null)
            {
                throw new ItemNotFoundException();
            }
            return item;
        }

        public async Task Update(UpdateItemDto itemDto)
        {
            var item = await _itemRepository.Get(itemDto.Id);
            if (item == null)
            {
                throw new ItemNotFoundException();
            }
            await _itemRepository.Update(item);
        }

        public async Task AddToShop(int itemId, int shopId)
        {
            var item = await _itemRepository.Get(itemId);
            if (item == null)
            {
                throw new ItemNotFoundException();
            }
            var shop = await _shopRepository.Get(shopId);
            if (shop == null)
            {
                throw new ShopNotFoundException();
            }
            item.ShopId = shopId;
            item.Shop = shop;
            await _itemRepository.Update(item);
        }
    }
}

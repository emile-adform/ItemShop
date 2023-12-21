using AutoMapper;
using ItemShop.Exceptions;
using ItemShop.Interfaces;
using ItemShop.Models.DTOs;
using ItemShop.Models.Entities;
using ItemShop.Repositories;

namespace ItemShop.Services
{
    public class ItemService
    {

        private readonly EFItemRepository _efItemRepository;
        private readonly IMapper _mapper;

        public ItemService(EFItemRepository itemRepository, IMapper mapper)
        {
            _efItemRepository = itemRepository;
            _mapper = mapper;
        }
        public async Task CreateItem(CreateItemDto item)
        {
            var entity = _mapper.Map<Item>(item);
            await _efItemRepository.Create(entity);
        }

        public async Task DeleteItem(int id)
        {
            var item = await _efItemRepository.Get(id);
            if(item == null)
            {
                throw new ItemNotFoundException();
            }
            await _efItemRepository.Delete(item);
        }

        public async Task<List<Item>> GetAllItems()
        {
            List<Item> items = new List<Item>(await _efItemRepository.Get());
            if(items.Count < 1)
            {
                throw new NoItemsFoundException();
            }
            return items;
        }

        public async Task<Item> GetItem(int id)
        {
            var item = await _efItemRepository.Get(id);
            if (item == null)
            {
                throw new ItemNotFoundException();
            }
            return item;
        }

        public async Task UpdateItem(UpdateItemDto itemDto)
        {
            var item = await _efItemRepository.Get(itemDto.Id);
            if (item == null)
            {
                throw new ItemNotFoundException();
            }
            var entity = _mapper.Map<Item>(itemDto);
            await _efItemRepository.Update(entity);
        }
    }
}

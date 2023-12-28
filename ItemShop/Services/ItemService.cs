using AutoMapper;
using ItemShop.Exceptions;
using ItemShop.Interfaces;
using ItemShop.Models.DTOs.ItemDtos;
using ItemShop.Models.Entities;

namespace ItemShop.Services
{
    public class ItemService
    {

        private readonly IItemRepository _efItemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _efItemRepository = itemRepository;
            _mapper = mapper;
        }
        public async Task Create(CreateItemDto item)
        {
            var entity = _mapper.Map<Item>(item);
            await _efItemRepository.Create(entity);
        }

        public async Task Delete(int id)
        {
            var item = await _efItemRepository.Get(id);
            if(item == null)
            {
                throw new ItemNotFoundException();
            }
            await _efItemRepository.Delete(item);
        }

        public async Task<List<Item>> GetAll()
        {
            List<Item> items = await _efItemRepository.Get();
            return items;
        }

        public async Task<Item> Get(int id)
        {
            var item = await _efItemRepository.Get(id);
            if (item == null)
            {
                throw new ItemNotFoundException();
            }
            return item;
        }

        public async Task Update(UpdateItemDto itemDto)
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

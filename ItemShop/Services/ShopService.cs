using AutoMapper;
using ItemShop.Exceptions;
using ItemShop.Models.DTOs.ItemDtos;
using ItemShop.Models.DTOs.ShopDtos;
using ItemShop.Models.Entities;
using ItemShop.Repositories;

namespace ItemShop.Services
{
    public class ShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;
        public ShopService(IShopRepository shopRepository, IMapper mapper)
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
        }
        public async Task Create(CreateShopDto shop)
        {
            var entity = _mapper.Map<Shop>(shop);
            _shopRepository.Create(entity);
        }
        public async Task Delete(int id)
        {
            var shop = await _shopRepository.Get(id);
            if (shop == null)
            {
                throw new ShopNotFoundException();
            }
            await _shopRepository.Delete(shop);
        }

        public async Task<List<Shop>> GetAll()
        {
            List<Shop> shops = await _shopRepository.Get();
            return shops;
        }

        public async Task<Shop> Get(int id)
        {
            var shop = await _shopRepository.Get(id);
            if (shop == null)
            {
                throw new ShopNotFoundException();
            }
            return shop;
        }

        public async Task Update(Shop shop)
        {
            var item = await _shopRepository.Get(shop.Id);
            if (item == null)
            {
                throw new ShopNotFoundException();
            }
            var entity = _mapper.Map<Shop>(shop);
            await _shopRepository.Update(entity);
        }
    }
}

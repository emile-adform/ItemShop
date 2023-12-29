using AutoMapper;
using ItemShop.Clients;
using ItemShop.Exceptions;
using ItemShop.Interfaces;
using ItemShop.Models.DTOs.PurchaseDto;
using ItemShop.Models.DTOs.ShopDtos;
using ItemShop.Models.Entities;
using ItemShop.Repositories;

namespace ItemShop.Services
{
    public class PurchaseService
    {
        private readonly PurchaseRepository _purchaseRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IJsonPlaceholderClient _client;
        public PurchaseService(PurchaseRepository purchaseRepository, IItemRepository itemRepository, IJsonPlaceholderClient client)
        {
            _purchaseRepository = purchaseRepository;
            _itemRepository = itemRepository;
            _client = client;
        }
        public async Task Create(int userId, int itemId)
        {
            var user = await _client.GetUserAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var item = await _itemRepository.Get(itemId);
            if (item == null)
            {
                throw new ItemNotFoundException();
            }
            var entity = new Purchase { UserId = userId, ItemId = itemId };
            _purchaseRepository.Create(entity);
        }
        public async Task<List<Purchase>> Get()
        {
            return await _purchaseRepository.Get();
        }
    }
}

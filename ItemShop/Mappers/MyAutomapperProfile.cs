using AutoMapper;
using ItemShop.Models.DTOs.ItemDtos;
using ItemShop.Models.DTOs.PurchaseDto;
using ItemShop.Models.DTOs.ShopDtos;
using ItemShop.Models.Entities;

namespace ItemShop.Mappers
{
    public class MyAutomapperProfile : Profile
    {
        public MyAutomapperProfile()
        {
            CreateMap<CreateItemDto, Item>().ReverseMap();
            CreateMap<UpdateItemDto, Item>().ReverseMap();
            CreateMap<CreateShopDto, Shop>().ReverseMap();
            CreateMap<UpdateShopDto, Shop>().ReverseMap();
        }
    }
}

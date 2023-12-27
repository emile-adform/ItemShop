using AutoMapper;
using ItemShop.Models.DTOs.ItemDtos;
using ItemShop.Models.Entities;

namespace ItemShop.Mappers
{
    public class MyAutomapperProfile : Profile
    {
        public MyAutomapperProfile()
        {
            CreateMap<CreateItemDto, Item>().ReverseMap();
            CreateMap<UpdateItemDto, Item>().ReverseMap();
        }
    }
}

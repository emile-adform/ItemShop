using AutoMapper;
using ItemShop.Models.DTOs;
using ItemShop.Models.Entities;

namespace ItemShop.Mappers
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<CreateItemDto, Item>().ReverseMap();
            CreateMap<UpdateItemDto, Item>().ReverseMap();
        }
    }
}

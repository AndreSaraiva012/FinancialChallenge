using AutoMapper;
using BuyRequestDomain.DTO_s;
using BuyRequestDomain.Entities;
using BuyRequestDomain.ViewModels;

namespace BuyRequestDomain.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<BuyRequest, BuyRequestDTO>().ReverseMap();
            CreateMap<ProductRequest, ProductRequestDTO>().ReverseMap();
        }
    }
}

using AutoMapper;
using ProductDomain.DTO_s;
using ProductDomain.Entities;

namespace ProductApplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}

using AutoMapper;
using DocumentDomain.DTO_s;
using DocumentDomain.Entities;

namespace DocumentApplication.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<DocumentDTO, Document>().ReverseMap();
        }
    }
}

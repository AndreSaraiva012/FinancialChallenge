using AutoMapper;
using BankRecordDomain.DTO_s;
using BankRecordDomain.Entities;

namespace BankRecordDomain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BankRecord, BankRecordDTO>().ReverseMap();
        }
    }
}

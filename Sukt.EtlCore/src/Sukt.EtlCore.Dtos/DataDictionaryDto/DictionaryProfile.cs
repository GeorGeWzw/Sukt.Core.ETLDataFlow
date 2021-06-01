using AutoMapper;
using Sukt.EtlCore.Domain.Models.SystemFoundation.DataDictionary;

namespace Sukt.EtlCore.Dtos.DataDictionaryDto
{
    public class DictionaryProfile : Profile
    {
        public DictionaryProfile()
        {
            CreateMap<DataDictionaryEntity, TreeDictionaryOutDto>().ForMember(x => x.title, opt => opt.MapFrom(x => x.Title));
        }
    }
}
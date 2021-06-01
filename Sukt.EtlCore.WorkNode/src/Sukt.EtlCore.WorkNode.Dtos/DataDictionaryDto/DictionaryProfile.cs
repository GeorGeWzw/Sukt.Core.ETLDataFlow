using AutoMapper;
using Sukt.EtlCore.WorkNode.Domain.Models.SystemFoundation.DataDictionary;

namespace Sukt.EtlCore.WorkNode.Dtos.DataDictionaryDto
{
    public class DictionaryProfile : Profile
    {
        public DictionaryProfile()
        {
            CreateMap<DataDictionaryEntity, TreeDictionaryOutDto>().ForMember(x => x.title, opt => opt.MapFrom(x => x.Title));
        }
    }
}
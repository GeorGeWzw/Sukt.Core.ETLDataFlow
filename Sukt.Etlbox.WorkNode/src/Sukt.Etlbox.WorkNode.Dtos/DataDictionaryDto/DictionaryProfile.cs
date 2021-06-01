using AutoMapper;
using Sukt.Etlbox.WorkNode.Domain.Models.SystemFoundation.DataDictionary;

namespace Sukt.Etlbox.WorkNode.Dtos.DataDictionaryDto
{
    public class DictionaryProfile : Profile
    {
        public DictionaryProfile()
        {
            CreateMap<DataDictionaryEntity, TreeDictionaryOutDto>().ForMember(x => x.title, opt => opt.MapFrom(x => x.Title));
        }
    }
}
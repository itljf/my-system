using AutoMapper;
using L.WInfoGroups;
using L.WInfoTags;

namespace L;

public class LApplicationAutoMapperProfile : Profile
{
    public LApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<WInformations.Information, WInformations.InformationDto>();
        CreateMap<WInformations.InformationEditDto, WInformations.Information>();
        CreateMap<WInformations.InformationDto, WInformations.InformationEditDto>();

        CreateMap<InfoGroup, InfoGroupDto>();
        CreateMap<InfoGroupEditDto, InfoGroup>();
        CreateMap<InfoGroupDto, InfoGroupEditDto>();
        CreateMap<InfoGroupItem, InfoGroupItemDto>();

        CreateMap<InfoTag, InfoTagDto>();
        CreateMap<InfoTagDto,InfoTagEditDto>();
        CreateMap<InfoTagEditDto,InfoTag>();
        CreateMap<InfoTagItem,InfoTagItemDto>();
    }
}

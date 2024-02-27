using AutoMapper;
using SGBI.SBGI.Core.DTOs;
using SGBI.SBGI.Core.DTOs.Tarifa;
using SGBI.SBGI.Core.Entities;
using SGBI.SGBI.Core.DTOs.Tramite;

namespace SGBI.SBGI.Core.Common.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Usuario, UsuarioLoginDto>().ReverseMap();
        CreateMap<UsuarioRegisterDto, Usuario>().ReverseMap();
        CreateMap<UsuarioLoginDto, UsuarioRegisterDto>().ReverseMap();


        CreateMap<Tarifa, TarifaRegisterDto>().ReverseMap();
        CreateMap<Tarifa, TarifaDto>().ReverseMap();
        CreateMap<TarifaDto, TarifaRegisterDto>();


        CreateMap<TramiteRegisterDto, Tramite>()
            .ReverseMap();


        CreateMap<Tramite, TramiteRegisterDto>()
            .ReverseMap()
            .ForMember(x => x.TramiteCampo, options => options.MapFrom(src => src.TramiteCampo))
            .AfterMap((src, dest) => dest.TramiteCampo.TramiteNombre = dest.Nombre);
    }
}
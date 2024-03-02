using AutoMapper;
using SGBI.SBGI.Core.DTOs;
using SGBI.SGBI.Core.DTOs.Tarifa;
using SGBI.SGBI.Core.DTOs.Tramite;
using SGBI.SGBI.Core.Entities;

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
            .ForMember(dest => dest.TramiteCampo, opt => opt.MapFrom(src => src.TramiteCampo))
            .ReverseMap();

        CreateMap<TramiteCampo, TramiteCampoRegisterDto>().ReverseMap();
    }
}
using AutoMapper;
using SGBI.SBGI.Core.DTOs;
using SGBI.SBGI.Core.DTOs.Tarifa;
using SGBI.SBGI.Core.Entities;

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
    }
}
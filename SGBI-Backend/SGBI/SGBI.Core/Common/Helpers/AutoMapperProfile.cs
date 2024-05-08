using AutoMapper;
using SGBI.SBGI.Core.DTOs;
using SGBI.SBGI.Core.DTOs.Tarifa;
using SGBI.SBGI.Core.Entities;
using SGBI.SGBI.Core.DTOs.Tarifa;
using SGBI.SGBI.Core.DTOs.Tramite;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.Registro;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesGenerados;
using SGBI.SGBI.Core.DTOs.Tramite.Salida;

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


        CreateMap<TramiteRegisterDto, Tramite>()
            .ForMember(dest => dest.TramiteCampo, opt => opt.MapFrom(src => src.TramiteCampo))
            .ReverseMap();

        CreateMap<TramiteCampo, TramiteCampoRegisterDto>().ReverseMap();

        CreateMap<TramiteInformacion, TramiteInformacionRegisterDto>().ReverseMap();

        CreateMap<TramiteInformacion, ObtenerInformacionTramiteDTO>().ReverseMap();

        CreateMap<TarifaMontoDescripcionDTO, Tarifa>().ReverseMap();
        
        CreateMap<ObtenerTramitesNombreIdDTO, Tramite>().ReverseMap();

        CreateMap<ObtenerCamposTramiteDTO, TramiteCampo>().ReverseMap();

        CreateMap<EstadoDTO, Tramite>().ReverseMap();

        CreateMap<TramiteInformacionActualizacionDTO, TramiteInformacion>().ReverseMap();

        CreateMap<BasuraInformacionDto, BasuraInformacion>().ReverseMap();


        CreateMap<Tramite, TramiteRegisterDto>();

        CreateMap<TarifaMonto, Tarifa>().ReverseMap();

        CreateMap<TramiteInformacion, ObtenerTramiteInformacionConTramiteDTO>()
            .ForMember(dest => dest.NombreTramite, opt => opt.MapFrom(src => src.Tramite.Nombre))
            .ForMember(dest => dest.CodigoTramite, opt => opt.MapFrom(src => src.Tramite.Codigo));

        CreateMap<ObtenerInformacionTramitePorIdDTO, TramiteInformacion>().ReverseMap();

        CreateMap<BasuraInformacionDto, BasuraInformacion>().ReverseMap();

        CreateMap<ExoneracionInformacionDto, ExoneracionInformacion>().ReverseMap();

        CreateMap<AseoInformacionDto, AseoInformacion>().ReverseMap();

        CreateMap<ParqueInformacionDto, ParqueInformacion>().ReverseMap();

        CreateMap<ExoneracionRegistroDto, TramiteInformacion>().ReverseMap();

        CreateMap<ServicioBasuraRegistroDto, TramiteInformacion>().ReverseMap();

        CreateMap<ServicioParquesOrnatoRegistroDto, TramiteInformacion>().ReverseMap();

        CreateMap<ServicioAseoViasRegistroDto, TramiteInformacion>().ReverseMap();

        CreateMap<ExoneracionRegistroDto, ExoneracionInformacion>().ReverseMap();

        CreateMap<BienesInmueblesRegistroDto, TramiteInformacion>().ReverseMap();

        CreateMap<BienesInmueblesInformacionDto, BienesInmueblesInformacion>().ReverseMap();

        CreateMap<ObtenerInformacionTramiteUnico, Tramite>().ReverseMap();

        
    }
}
using Microsoft.AspNetCore.Mvc;
using SGBI.SGBI.Core.DTOs.Tramite;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.Registro;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesGenerados;
using SGBI.SGBI.Core.DTOs.Tramite.Salida;

namespace SGBI.SBGI.Core.Interfaces;

public interface ITramiteService
{
    Task<string> RegistraNuevoTramiteAsync(TramiteRegisterDto tramiteDto);

    Task<ActionResult<ObtenerCamposTramiteDTO>> ObtenerCamposDelTramiteAsync(ObtenerTramitesDTO obtenerTramitesDTO);
    
    Task<string> UsarTramiteAsync(TramiteInformacionRegisterDto tramiteInformacionRegisterDto);

    Task<ActionResult<List<ObtenerInformacionTramiteDTO>>> ObtenerTramiteAsync(ObtenerTramitesDTO obtenerTramitesDTO);

    Task<ActionResult<List<ObtenerTramitesNombreIdDTO>>> ObtenerTramiteNombreIdAsync();

    Task<string> CambiarEstadoTramiteAsync(CambiarEstadoTramiteDTO cambiarEstadoTramiteDTO);



    // ACTUALIZACION DE TRAMITES ------------------------------------------------------------------------
    Task<string> ActualizarCamposDelTramiteAsync(int id, TramiteCampoRegisterDto tramiteCampoRegisterDto);

    Task<string> ActualizarInformacionDelTramiteAsync(int id, TramiteInformacionActualizacionDTO tramiteInformacionActualizacionDTO);

    Task<string> ActualizarTramiteBasuraAsync(int id, ServicioBasuraRegistroDto servicioBasuraRegistroDTO);

    Task<string> ActualizarTramiteExoneracionAsync(int id, ExoneracionRegistroDto exoneracionRegistroDTO);

    Task<string> ActualizarTramiteAseoViasAsync(int id, ServicioAseoViasRegistroDto servicioAseoViasRegistroDTO);

    Task<string> ActualizarTramiteParquesOrnatoAsync(int id, ServicioParquesOrnatoRegistroDto servicioParquesOrnatoRegistroDTO);

    Task<string> ActualizarTramiteBienesInmueblesAsync(int id, BienesInmueblesRegistroDto bienesInmueblesRegistroDTO);

    Task<string>ActualizarTramiteAsync(int id, TramiteRegisterDto tramiteRegisterDto);



    // OBTENCION DE TRAMITES ------------------------------------------------------------------------

    Task<List<TramiteRegisterDto>> ObtenerTodosLosTramitesAsync();

    Task<List<ObtenerTramiteInformacionConTramiteDTO>> ObtenerTodoTramiteInformacionAsync();

    Task<ObtenerInformacionTramitePorIdDTO> ObtenerInformacionTramitePorIdAsync(int id);

    Task<ExoneracionRegistroDto> ObtenerTramiteExoneracionInformacionPorIdAsync(int id);

    Task<ServicioAseoViasRegistroDto> ObtenerTramiteServicioAseoViasInformacionPorIdAsync(int id);

    Task<ServicioBasuraRegistroDto> ObtenerTramiteServicioBasuraInformacionPorIdAsync(int id);

    Task<ServicioParquesOrnatoRegistroDto> ObtenerTramiteServicioParqueOrnatoInformacionPorIdAsync(int id);

    Task<BienesInmueblesRegistroDto> ObtenerTramiteBienesInmueblesInformacionPorIdAsync(int id);

    Task<ObtenerInformacionTramiteUnico> ObtenerInformacionTramiteUnicoAsync(int id);

    // INGRESO DE TRAMITES ------------------------------------------------------------------------
    Task<string> RegistrarExoneracion(ExoneracionRegistroDto exoneracionInformacionDto);

    Task<string> RegistrarBasura(ServicioBasuraRegistroDto servicioBasuraRegistroDto);

    Task<string> RegistrarParquesObras(ServicioParquesOrnatoRegistroDto servicioParquesOrnatoRegistroDto);

    Task<string> RegistrarAseoVias(ServicioAseoViasRegistroDto servicioAseoViasRegistroDto);

    Task<string> RegistrarTramiteBienesInmuebles(BienesInmueblesRegistroDto bienesImueblesRegistroDto);


    // BORRAR TRAMITE ------------------------------------------------------------------------
    Task<string> BorrarTramiteInformacionAsync(int id);
    
}
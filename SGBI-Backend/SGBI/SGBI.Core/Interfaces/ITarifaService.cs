using SGBI.SBGI.Core.DTOs.Tarifa;
using SGBI.SGBI.Core.DTOs.Tarifa;

namespace SGBI.SGBI.Core.Interfaces;

public interface ITarifaService
{
    Task<string> RegistrarNuevaTarifaAsync(TarifaRegisterDto tarifaRegisterDto);

    Task<List<TarifaDto>> ListarTarifasAsync();

    Task<List<TarifaDto>> ListarTarifaExonerarAsync();

    Task<List<TarifaDto>> ListarTarifaMantenimientoAsync();

    Task<List<TarifaDto>> ListarTarifaServiciosAseoAsync();

    Task<List<TarifaDto>> ListarTarifaServiciosBasuraAsync();

    Task<List<TarifaMonto>> ObtenerUltimosTarifa_MontosAsync(string descripcion);
}
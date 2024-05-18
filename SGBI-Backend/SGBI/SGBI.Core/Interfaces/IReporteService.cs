using Microsoft.AspNetCore.Mvc;
using SGBI.SBGI.Core.Entities;
using SGBI.SGBI.API.Models;
using SGBI.SGBI.Core.DTOs.Reporte;

namespace SGBI.SBGI.Core.Interfaces;

public interface IReporteService
{
    public byte[] GenerarPDFDeUsuarios();
    public byte[] GenerarExcelDeUsuarios();

    public Task<ObtenerConteoTramitesDTO> ObtenerConteoTramitesAsync(DateTime? FechaInicio, DateTime? FechaFinal, bool? SoloRolSeleccionado, string? Rol, string? Usuario);
    public Task<List<ReporteExoneracionDTO>> ReporteExoneracionAsync(DateTime? mes);
    public Task<ReporteContaduriaDTO> ObtenerReporteContaduriaBienesInmueblesAsync(int Ano, bool? SoloRolSeleccionado, string? Rol);
    public Task<ReporteContaduriaDTO> ObtenerReporteContaduriaBasuraResidencialAsync(int Ano, bool? SoloRolSeleccionado, string? Rol);
    public Task<ReporteContaduriaDTO> ObtenerReporteContaduriaAseoViasAsync(int Ano, bool? SoloRolSeleccionado, string? Rol);
    public Task<ReporteContaduriaDTO> ObtenerReporteContaduriaMantenimientoParqueAsync(int Ano, bool? SoloRolSeleccionado, string? Rol);
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SGBI.SBGI.Core.DTOs;
using SGBI.SBGI.Core.Entities;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.API.Services;
using SGBI.SGBI.Core.DTOs.Reporte;

namespace SGBI.SGBI.API.Controllers;

[ApiController]
[Route("api/reporte")]
public class ReporteController : ControllerBase
{
    private readonly IReporteService _reporteService;

    public ReporteController(IReporteService reporteService)
    {
        _reporteService = reporteService;
    }

    [HttpGet("GenerateReportePDF-TESTING")]
    public IActionResult GenerarPDFDeUsuarios() //TESTING METHOD ONLY
    {
        try
        {
            var pdfBytes = _reporteService.GenerarPDFDeUsuarios();
            return File(pdfBytes, "application/pdf", "report.pdf");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }
    }

    [HttpGet("GenerarReporteExcel-TESTING")]
    public IActionResult GenerarExcelDeUsuarios() //TESTING METHOD ONLY
    {
        try
        {
            var excelBytes = _reporteService.GenerarExcelDeUsuarios();
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reporte.xlsx");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }
    }


    [HttpGet("ConteoDeTramitePorRangoDeFechas")]
    public async Task<IActionResult> ConteoDeTramitePorRangoDeFechas([FromQuery] DateTime? FechaInicio, DateTime? FechaFinal, bool? SoloRolSeleccionado, string? Rol, string? Usuario)
    {
        try
        {
            var ConteoTramites = await _reporteService.ObtenerConteoTramitesAsync(FechaInicio, FechaFinal, SoloRolSeleccionado, Rol, Usuario);

            return Ok(ConteoTramites);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }
    }

    [HttpGet("ObtenerReporteContaduriaBienesInmuebles")]
    public async Task<IActionResult> ObtenerReporteContaduriaBienesInmuebles([FromQuery] int Ano, bool? SoloRolSeleccionado, string? Rol)
    {

        try
        {
            var ReporteContaduria = await _reporteService.ObtenerReporteContaduriaBienesInmueblesAsync(Ano, SoloRolSeleccionado, Rol);

            return Ok(ReporteContaduria);
        }
        catch(Exception ex)
        {
            return StatusCode(500, new {Message = ex.Message, Error = ex.Message});
        }

    }

    [HttpGet("ReporteExoneracion")]
    public async Task<IActionResult> ReporteExoneracion(DateTime? mes)
    {
        try
        {
            var reporte = await _reporteService.ReporteExoneracionAsync(mes);

            return Ok(reporte);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }
    }

    [HttpGet("ObtenerReporteContaduriaBasuraResidencial")]
    public async Task<IActionResult> ObtenerReporteContaduriaBasuraResidencial([FromQuery] int Ano, bool? SoloRolSeleccionado, string? Rol)
    {

        try
        {
            var ReporteContaduria = await _reporteService.ObtenerReporteContaduriaBasuraResidencialAsync(Ano, SoloRolSeleccionado, Rol);

            return Ok(ReporteContaduria);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }

    }

    [HttpGet("ObtenerReporteContaduriaAseoVias")]
    public async Task<IActionResult> ObtenerReporteContaduriaAseoVias([FromQuery] int Ano, bool? SoloRolSeleccionado, string? Rol)
    {

        try
        {
            var ReporteContaduria = await _reporteService.ObtenerReporteContaduriaAseoViasAsync(Ano, SoloRolSeleccionado, Rol);

            return Ok(ReporteContaduria);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }

    }

    [HttpGet("ObtenerReporteContaduriaMantenimientoParque")]
    public async Task<IActionResult> ObtenerReporteContaduriaMantenimientoParque([FromQuery] int Ano, bool? SoloRolSeleccionado, string? Rol)
    {

        try
        {
            var ReporteContaduria = await _reporteService.ObtenerReporteContaduriaMantenimientoParqueAsync(Ano, SoloRolSeleccionado, Rol);

            return Ok(ReporteContaduria);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }

    }

}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGBI.SBGI.Core.DTOs.Tarifa;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.API.Services;
using SGBI.SGBI.Core.DTOs.Tarifa;
using SGBI.SGBI.Core.DTOs.Tramite;
using SGBI.SGBI.Core.Interfaces;

namespace SGBI.SGBI.API.Controllers;

[ApiController]
[Route("api/tarifa")]
public class TarifaController : ControllerBase
{
    private readonly ITarifaService _tarifaService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TarifaController(ITarifaService tarifaService, IHttpContextAccessor httpContextAccessor)
    {
        _tarifaService = tarifaService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    
    [Authorize(Roles = "Administrador, Jefe")]
    [HttpPost("registrar")] //Endpoint para registrar nuevas tarifas
    public async Task<IActionResult> Registrar([FromBody] TarifaRegisterDto? tarifaDto)
    {
        if (tarifaDto == null)
            return BadRequest(new { Message = "Datos inválidos" });

        try
        {

            // Use userName as needed
            var tarifaRegistrada = await _tarifaService.RegistrarNuevaTarifaAsync(tarifaDto);
            

            return Ok(new
            {
                Message = tarifaRegistrada
            });
            
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe")]
    [HttpGet("listar")] //Endpoint para listar tarifas
    public async Task<IActionResult> Listar()
    {
        try
        {
            var tarifas = await _tarifaService.ListarTarifasAsync();

            return Ok(tarifas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe")]
    [HttpGet("listar/exonerar")] //Endpoint para listar tarifas
    public async Task<IActionResult> ListarExonerar()
    {
        try
        {
            var tarifas = await _tarifaService.ListarTarifaExonerarAsync();

            return Ok(tarifas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe")]
    [HttpGet("listar/mantenimiento")] //Endpoint para listar tarifas
    public async Task<IActionResult> ListarMantenimiento()
    {
        try
        {
            var tarifas = await _tarifaService.ListarTarifaMantenimientoAsync();

            return Ok(tarifas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }
    }
    [Authorize(Roles = "Administrador, Jefe")]
    [HttpGet("listar/servicios-aseo")] //Endpoint para listar tarifas
    public async Task<IActionResult> ListarServiciosAseo()
    {
        try
        {
            var tarifas = await _tarifaService.ListarTarifaServiciosAseoAsync();

            return Ok(tarifas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }
    }
    [Authorize(Roles = "Administrador, Jefe")]
    [HttpGet("listar/servicios-basura")] //Endpoint para listar tarifas
    public async Task<IActionResult> ListarServiciosBasura()
    {
        try
        {
            var tarifas = await _tarifaService.ListarTarifaServiciosBasuraAsync();

            return Ok(tarifas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerUltimosMontoYTarifas")]
    public async Task<ActionResult<List<TarifaMonto>>> ObtenerUltimosTarifa_Montos([FromQuery] string descripcion)
    {
        if (descripcion == null)
        {
            return BadRequest("Datos inválidos");
        }

        try
        {
            var tarifaInformacion = await _tarifaService.ObtenerUltimosTarifa_MontosAsync(descripcion);

            if (tarifaInformacion != null)
            {
                return tarifaInformacion;
            }
            else
            {
                return NotFound("No existe el monto o tarifa buscado");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }
}
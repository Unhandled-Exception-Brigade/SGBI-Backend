using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGBI.SBGI.Core.DTOs.Tarifa;
using SGBI.SBGI.Core.Interfaces;

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
    
    
    [Authorize(Roles = "Administrador")]
    [HttpPost("registrar")] //Endpoint para registrar nuevas tarifas
    public async Task<IActionResult> Registrar([FromBody] TarifaRegisterDto? tarifaDto)
    {
        if (tarifaDto == null)
            return BadRequest(new { Message = "Datos inválidos" });

        try
        {
            // if (!User.Identity!.IsAuthenticated)
            // {
            //     return Unauthorized(new { Message = "Usuario no autenticado" });
            // }
            
            if (HttpContext == null)
            {
                return StatusCode(500, new { Message = "Error en el servidor", Error = "HttpContext is null" });
            }

            

            // Use userName as needed
            var tarifaRegistrada = await _tarifaService.RegistrarNuevaTarifaAsync(tarifaDto);
            
            if (tarifaRegistrada != "Tarifa Creada" && tarifaRegistrada != "Tarifa Actualizada")
                return BadRequest(new { Message = tarifaRegistrada });

            return Ok(new
            {
                Message = tarifaRegistrada
            });
            
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }


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
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }

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
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }

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
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }

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
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }

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
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.Core.DTOs.Tramite;

namespace SGBI.SGBI.API.Controllers;

[ApiController]
[Route("api/tramite")]
public class TramiteController : ControllerBase
{
    private readonly ITramiteService _tramiteService;

    public TramiteController(ITramiteService tramiteService)
    {
        _tramiteService = tramiteService;
    }

    //[Authorize(Roles = "Administrador, Jefe")]
    [HttpPost("registrar")] //Endpoint para registrar nuevas tarifas
    public async Task<IActionResult> Registrar([FromBody] TramiteRegisterDto tramiteDto)
    {
        if (tramiteDto == null)
            return BadRequest(new { Message = "Datos inválidos" });

        try
        {
            var tramiteRegistrado = await _tramiteService.RegistraNuevoTramiteAsync(tramiteDto);

            if (tramiteRegistrado != "Tramite Creado")

                return BadRequest(new { Message = tramiteRegistrado });


            return Ok(new
            {
                Message = tramiteRegistrado
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }
}
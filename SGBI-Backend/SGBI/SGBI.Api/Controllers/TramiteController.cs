using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Operators;
using SGBI.SBGI.Core.Entities;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.Core.DTOs.Tramite;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.Registro;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesGenerados;

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

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("registrar")] //Endpoint para registrar nuevas tarifas
    public async Task<IActionResult> Registrar([FromBody] TramiteRegisterDto tramiteDto)
    {
        if (tramiteDto == null)
            return BadRequest(new { Message = "Datos invalidos" });

        try
        {
            var tramiteRegistrado = await _tramiteService.RegistraNuevoTramiteAsync(tramiteDto);


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

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerCamposDelTramite")]
    public async Task<ActionResult<ObtenerCamposTramiteDTO>> ObtenerCamposDelTramite(
        [FromQuery] ObtenerTramitesDTO obtenerTramitesDTO)
    {
        if (obtenerTramitesDTO == null) return BadRequest(new { Message = "Datos invalidos" });

        try

        {
            var tramiteCampos = await _tramiteService.ObtenerCamposDelTramiteAsync(obtenerTramitesDTO);

            if (tramiteCampos == null) return NotFound("Tramite inexistente");

            return tramiteCampos;
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("UsarTramite")]
    public async Task<IActionResult> UsarTramite([FromBody] TramiteInformacionRegisterDto tramiteInformacionRegisterDto)
    {
        if (tramiteInformacionRegisterDto == null) return BadRequest("Informacion invalida");

        try
        {
            var tramiteInformacion = await _tramiteService.UsarTramiteAsync(tramiteInformacionRegisterDto);

            if (tramiteInformacion == "Tramite inexistente")
                return BadRequest("No existe el tramite que se quiere utilizar");


            return Ok(new
            {
                Message = tramiteInformacion
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("UsarExoneracion")]
    public async Task<IActionResult> UsarExoneracion([FromBody] ExoneracionRegistroDto exoneracionRegistroDto)
    {
        if (exoneracionRegistroDto == null) return BadRequest("Informacion invalida");

        try
        {
            var exoneracion = await _tramiteService.RegistrarExoneracion(exoneracionRegistroDto);

            if (exoneracion == "Tramite inexistente")
                return BadRequest("No existe el tramite que se quiere utilizar");

            return Ok(new
            {
                Message = exoneracion
            });

        }catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("UsarServicioBasura")]
    public async Task<IActionResult> UsarServicioBasura([FromBody] ServicioBasuraRegistroDto servicioBasuraRegistroDto)
    {
        if (servicioBasuraRegistroDto == null) return BadRequest("Informacion invalida");

        try
        {
            var exoneracion = await _tramiteService.RegistrarBasura(servicioBasuraRegistroDto);

            if (exoneracion == "Tramite inexistente")
                return BadRequest("No existe el tramite que se quiere utilizar");

            return Ok(new
            {
                Message = exoneracion
            });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerInformacionTramiteUnico")]
    public async Task<IActionResult> ObtenerInformacionTramiteUnico([FromQuery] int id)
    {
        try
        {
            var tramiteInformacion = await _tramiteService.ObtenerInformacionTramiteUnicoAsync(id);

            if (tramiteInformacion == null)
            {
                return BadRequest("No existe el tramite que se quiere utilizar");
            }
            return Ok(tramiteInformacion);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("UsarParquesObras")]
    public async Task<IActionResult> RegistrarParquesObras(ServicioParquesOrnatoRegistroDto servicioParquesOrnatoRegistroDto)
    {
        if(servicioParquesOrnatoRegistroDto == null) return BadRequest("Informacion invalida");

        try
        {
            var exoneracion = await _tramiteService.RegistrarParquesObras(servicioParquesOrnatoRegistroDto);

            if (exoneracion == "Tramite inexistente")
                return BadRequest("No existe el tramite que se quiere utilizar");

            return Ok(new
            {
                Message = exoneracion
            });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("UsarAseoVias")]
    public async Task<IActionResult> ServicioAseoViasRegistroDto(ServicioAseoViasRegistroDto servicioAseoViasRegistroDto)
    {
        if (servicioAseoViasRegistroDto == null) return BadRequest("Informacion invalida");

        try
        {
            var exoneracion = await _tramiteService.RegistrarAseoVias(servicioAseoViasRegistroDto);

            if (exoneracion == "Tramite inexistente")
                return BadRequest("No existe el tramite que se quiere utilizar");

            return Ok(new
            {
                Message = exoneracion
            });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    //[Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("UsarTramiteBienesInmuebles")]
    public async Task<IActionResult> BienesInmueblesRegistroDto(BienesInmueblesRegistroDto bienesInmueblesRegistroDto)
    {
        if (bienesInmueblesRegistroDto == null) return BadRequest("Informacion invalida");

        try
        {
            var tramite = await _tramiteService.RegistrarTramiteBienesInmuebles(bienesInmueblesRegistroDto);

            if (tramite == "Tramite inexistente")
                return BadRequest("No existe el tramite que se quiere utilizar");

            return Ok(new
            {
                Message = tramite
            });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerTramite")]
    public async Task<ActionResult<List<ObtenerInformacionTramiteDTO>>> ObtenerTramite(
        [FromQuery] ObtenerTramitesDTO obtenerTramitesDTO)
    {
        if (obtenerTramitesDTO == null) return BadRequest("Datos invalidos");

        try
        {
            var tramiteInformacion = await _tramiteService.ObtenerTramiteAsync(obtenerTramitesDTO);

            if (tramiteInformacion != null)
                return tramiteInformacion;
            return NotFound("No existe el tramite seleccionado");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }
    
    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerTramiteNombreyId")]
    public async Task<ActionResult<List<ObtenerTramitesNombreIdDTO>>> ObtenerTramiteNombreId()
    {
        try
        {
            var tramiteInformacion = await _tramiteService.ObtenerTramiteNombreIdAsync();

            if (tramiteInformacion != null)
                return tramiteInformacion;
            return NotFound("No existe ningun tramite seleccionado");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe")]
    [HttpPost("CambiarEstadoTramite")]
    public async Task<IActionResult> CambiarEstadoTramite([FromBody] CambiarEstadoTramiteDTO cambiarEstadoTramiteDTO)
    {
        try
        {
            var tramite = await _tramiteService.CambiarEstadoTramiteAsync(cambiarEstadoTramiteDTO);

            return Ok(new
            {
                Message = tramite
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe")]
    [HttpPost("ActualizarCamposTramite")]
    public async Task<IActionResult> ActualizarCamposDelTramite(int id,
        [FromBody] TramiteCampoRegisterDto tramiteCampoRegisterDto)
    {
        try
        {
            var tramite = await _tramiteService.ActualizarCamposDelTramiteAsync(id, tramiteCampoRegisterDto);

            return Ok(new
            {
                Message = tramite
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("ActualizarTramiteInformacion/{id}")]
    public async Task<IActionResult> ActualizarInformacionDelTramite(int id,
        [FromBody] TramiteInformacionActualizacionDTO tramiteInformacionActualizacionDTO)
    {
        try
        {
            var tramite =
                await _tramiteService.ActualizarInformacionDelTramiteAsync(id, tramiteInformacionActualizacionDTO);

            return Ok(new
            {
                Message = tramite
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("ActualizarTramiteBasura/{id}")]
    public async Task<IActionResult> ActualizarTramiteBasuraAsync(int id,
        [FromBody] ServicioBasuraRegistroDto servicioBasuraRegistroDto)
    {
        try
        {
            var tramite =
                await _tramiteService.ActualizarTramiteBasuraAsync(id, 
                servicioBasuraRegistroDto);

            return Ok(new
            {
                Message = tramite
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("ActualizarTramiteExoneracion/{id}")]
    public async Task<IActionResult> ActualizarTramiteExoneracionAsync(int id,
        [FromBody] ExoneracionRegistroDto exoneracionRegistroDto)
    {
        try
        {
            var tramite =
                await _tramiteService.ActualizarTramiteExoneracionAsync(id,
                exoneracionRegistroDto);

            return Ok(new
            {
                Message = tramite
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("ActualizarTramiteAseoVias/{id}")]
    public async Task<IActionResult> ActualizarTramiteAseoViasAsync(int id,
       [FromBody] ServicioAseoViasRegistroDto servicioAseoViasRegistroDTO)
    {
        try
        {
            var tramite =
                await _tramiteService.ActualizarTramiteAseoViasAsync(id,
                servicioAseoViasRegistroDTO);

            return Ok(new
            {
                Message = tramite
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("ActualizarTramiteParquesOrnato/{id}")]
    public async Task<IActionResult> ActualizarTramiteParquesOrnatoAsync(int id,
   [FromBody] ServicioParquesOrnatoRegistroDto servicioParquesOrnatoRegistroDTO)
    {
        try
        {
            var tramite =
                await _tramiteService.ActualizarTramiteParquesOrnatoAsync(id,
                servicioParquesOrnatoRegistroDTO);

            return Ok(new
            {
                Message = tramite
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpPost("ActualizarTramiteBienesInmuebles/{id}")]
    public async Task<IActionResult> ActualizarTramiteBienesInmueblesAsync(int id,
    [FromBody] BienesInmueblesRegistroDto bienesInmueblesRegistroDTO)
    {
        try
        {
            var tramite =
                await _tramiteService.ActualizarTramiteBienesInmueblesAsync(id,
                bienesInmueblesRegistroDTO);

            return Ok(new
            {
                Message = tramite
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe")]
    [HttpPost("actualizar/{id}")]
    public async Task<IActionResult> ActualizarTramite(int id, [FromBody] TramiteRegisterDto tramiteRegisterDto)
    {
        try
        {
            var tramite = await _tramiteService.ActualizarTramiteAsync(id, tramiteRegisterDto);

            return Ok(new
            {
                Message = tramite
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerTodosLosTramites")]
    public async Task<IActionResult> ObtenerTodosLosTramites()
    {
        try
        {
            var tramites = await _tramiteService.ObtenerTodosLosTramitesAsync();

            if (tramites != null)

                return Ok(tramites);

            return NotFound("No existen tramites");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerInformacionTramitePorId/{id}")]
    public async Task<IActionResult> ObtenerInformacionTramitePorId(int id)
    {
        try
        {
            var tramites = await _tramiteService.ObtenerInformacionTramitePorIdAsync(id);

            if (tramites != null)

                return Ok(tramites);

            return NotFound("No existen tramites");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerTodoTramiteInformacion")]
    public async Task<IActionResult> ObtenerTodoTramiteInformacion()
    {
        try
        {
            var tramites = await _tramiteService.ObtenerTodoTramiteInformacionAsync();

            if (tramites != null)

                return Ok(tramites);

            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerTramiteExoneracionPorId/{id}")]
    public async Task<IActionResult> ObtenerTramiteExoneracionInformacionPorId(int id)
    {
        try
        {
            var tramites = await _tramiteService.ObtenerTramiteExoneracionInformacionPorIdAsync(id);

            if (tramites != null)

                return Ok(tramites);

            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerTramiteAseoViasPorId/{id}")]
    public async Task<IActionResult> ObtenerTramiteServicioAseoViasInformacionPorId(int id)
    {
        try
        {
            var tramites = await _tramiteService.ObtenerTramiteServicioAseoViasInformacionPorIdAsync(id);

            if (tramites != null)

                return Ok(tramites);

            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerTramiteBasuraPorId/{id}")]
    public async Task<IActionResult> ObtenerTramiteServicioBasuraInformacionPorId(int id)
    {
        try
        {
            var tramites = await _tramiteService.ObtenerTramiteServicioBasuraInformacionPorIdAsync(id);

            if (tramites != null)

                return Ok(tramites);

            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerTramiteParquesOrnatoPorId/{id}")]
    public async Task<IActionResult> ObtenerTramiteServicioParquesOrnatoInformacionPorId(int id)
    {
        try
        {
            var tramites = await _tramiteService.ObtenerTramiteServicioParqueOrnatoInformacionPorIdAsync(id);

            if (tramites != null)

                return Ok(tramites);

            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
    [HttpGet("ObtenerTramiteBienesInmueblesPorId/{id}")]
    public async Task<IActionResult> ObtenerTramiteBienesInmueblesInformacionPorId(int id)
    {
        try
        {
            var tramites = await _tramiteService.ObtenerTramiteBienesInmueblesInformacionPorIdAsync(id);

            if (tramites != null)

                return Ok(tramites);

            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, Error = ex.Message });
        }
    }

}
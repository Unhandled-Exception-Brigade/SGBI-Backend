using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGBI.SBGI.Core.DTOs.Tarifa;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.API.Services;
using SGBI.SGBI.Core.DTOs.CodigoDepartamento.Entrada;
using SGBI.SGBI.Core.Interfaces;

namespace SGBI.SGBI.Api.Controllers
{
    [ApiController]
    [Route("api/codigoDepartamento")]
    public class CodigoDepartamentoController: ControllerBase
    {
        private readonly ICodigoDepartamentoService _codigoDepartamentoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CodigoDepartamentoController(ICodigoDepartamentoService codigoDepartamentoService, IHttpContextAccessor httpContextAccessor)
        {
            _codigoDepartamentoService = codigoDepartamentoService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
        [HttpPost("agregar")]
        public async Task<IActionResult> RegistrarNuevoCodigo([FromBody] AgregarCodigoDTO? agregarCodigoDTO)
        {
            if (agregarCodigoDTO == null)
                return BadRequest(new { Message = "Datos inválidos" });

            try
            {

                // Use userName as needed
                var codigoRegistro = await _codigoDepartamentoService.RegistrarNuevoCodigoAsync(agregarCodigoDTO);


                return Ok(new
                {
                    Message = codigoRegistro
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
            }
        }

        //[Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
        [HttpPost("actualizar")]
        public async Task<IActionResult> ActualizarCodigoDepartamento(int id, [FromBody] EditarCodigoDepartamentoDTO? editarCodigoDepartamentoDTO)
        {
            if (editarCodigoDepartamentoDTO == null)
                return BadRequest(new { Message = "Datos inválidos" });

            try
            {
                // Use userName as needed
                var codigoRegistro = await _codigoDepartamentoService.ActualizarCodigoDepartamento(id, editarCodigoDepartamentoDTO);

                return Ok(new
                {
                    Message = codigoRegistro
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
            }
        }

        [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
        [HttpGet("listarCodigos")]
        public async Task<IActionResult> ListarCodigos()
        {
            
            try
            {
                var codigos = await _codigoDepartamentoService.ListarCodigosAsync();

                return Ok(codigos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
            }
            
        }

        [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
        [HttpGet("codigosDropdown")]
        public async Task<IActionResult> ListarCodigoDescripcion()
        {

            try
            {
                var codigos = await _codigoDepartamentoService.ListarCodigoDescripcionAsync();

                return Ok(codigos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
            }

        }

        [HttpGet("obtenerUnCodigo")]
        public async Task<IActionResult> ObtenerUnCodigo(int id)
        {
            try
            {
                var codigo = await _codigoDepartamentoService.ObtenerUnCodigoAsync(id);

                return Ok(codigo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message, Error = ex.Message });
            }
        }

    }
}

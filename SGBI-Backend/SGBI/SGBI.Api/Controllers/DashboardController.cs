using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGBI.SGBI.Core.Interfaces;

namespace SGBI.SGBI.Api.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;


        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;

        }

        [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
        [HttpGet("ObtenerTramitesUltimoDiaPorUsuario")]
        public async Task<IActionResult> ObtenerTramitesUltimoDiaPorUsuario(string cedula)
        {

            try
            {
                var tramites = await _dashboardService.TramitesRealizadosUltimoDiaAsync(cedula);

                return Ok(new
                {
                    Message = tramites
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, Error = ex.Message });
            }
        }

        [Authorize(Roles = "Administrador, Jefe, Usuario, Depuracion")]
        [HttpGet("ObtenerCantidadDeTramitesEnElSistema")]
        public async Task<IActionResult> ObtenerCantidadDeTramitesEnElSistema(string cedula)
        {
            try
            {
                var tramites = await _dashboardService.ObtenerCantidadDeTramitesEnElSistemaAsync(cedula);

                return Ok(new
                {
                    Message = tramites
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, Error = ex.Message });
            }
        }


        [HttpGet("ObtenerTramitesPorUsuarioEnLaSemana")]
        public async Task<IActionResult> ObtenerTramitesPorUsuarioEnLaSemana(string cedula)
        {

            try
            {
                var tramites = await _dashboardService.TramitesRealizadosPorUsuarioEnLaSemanaAsync(cedula);

                return Ok(new
                {
                    Message = tramites
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, Error = ex.Message });
            }
        }

        [HttpGet("ObtenerTramitesPorUsuarioEnElAnio")]
        public async Task<IActionResult> ObtenerTramitesPorUsuarioEnElAnio(string cedula)
        {

            try
            {
                var tramites = await _dashboardService.TramitesRealizadosPorUsuarioEnElAnioAsync(cedula);

                return Ok(new
                {
                    Message = tramites
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, Error = ex.Message });
            }
        }

        [HttpGet("ObtenerTramitesActivos")]
        public async Task<IActionResult> ObtenerTramitesActivos()
        {

            try
            {
                var tramites = await _dashboardService.TramitesActivosAsync();

                return Ok(new
                {
                    Message = tramites
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, Error = ex.Message });
            }
        }

        [HttpGet("ObtenerPorcentajeTramitesPorUsuarioEnLaSemana")]
        public async Task<IActionResult> ObtenerPorcentajeTramitesPorUsuarioEnLaSemana(string cedula)
        {

            try
            {
                var tramites = await _dashboardService.PorcentajeRealizadosPorUsuarioEnLaSemanaAsync(cedula);

                return Ok(new
                {
                    Message = tramites
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, Error = ex.Message });
            }
        }

        [HttpGet("ObtenerInclusionesAnuales")]
        public async Task<IActionResult> ObtenerInclusionesAnuales(string cedula)
        {

            try
            {
                var exoneraciones = await _dashboardService.InclusionesAnualesAsync(cedula);

                return Ok(new
                {
                    Message = exoneraciones
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, Error = ex.Message });
            }
        }

        [HttpGet("ObtenerExclusionesAnuales")]
        public async Task<IActionResult> ObtenerExclusionesAnuales(string cedula)
        {

            try
            {
                var exoneraciones = await _dashboardService.ExclusionesAnualesAsync(cedula);

                return Ok(new
                {
                    Message = exoneraciones
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, Error = ex.Message });
            }
        }

        [HttpGet("ObtenerPorcentajesTramites")]
        public async Task<IActionResult> ObtenerPorcentajesTramites(string cedula)
        {

            try
            {
                var exoneraciones = await _dashboardService.PorcentajeTramitesAsync(cedula);

                return Ok(new
                {
                    Message = exoneraciones
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message, Error = ex.Message });
            }

        }
    }
}

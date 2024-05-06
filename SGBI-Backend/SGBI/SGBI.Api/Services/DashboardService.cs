using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGBI.SGBI.API.Data;
using SGBI.SGBI.Core.DTOs.Dashboard;
using SGBI.SGBI.Core.Interfaces;

namespace SGBI.SGBI.Api.Services
{
    public class DashboardService : IDashboardService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DashboardService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> TramitesRealizadosUltimoDiaAsync(string cedula)
        {
            if (cedula == null)
            {
                return 0;
            }

            // Obtener la fecha y hora hace 24 horas en UTC
            DateTime hace24HorasUtc = DateTime.UtcNow.AddHours(-24);

            // Filtrar los trámites realizados por el usuario en las últimas 24 horas
            var tramites = await _context.TramitesInformacion
                                        .Where(t => t.UsuarioCreacion == cedula && t.FechaCreacion >= hace24HorasUtc)
                                        .CountAsync();

            return tramites;
        }

        public async Task<int> ObtenerCantidadDeTramitesEnElSistemaAsync(string cedula)
        {
            var tramites = await _context.TramitesInformacion.Where(t => t.UsuarioCreacion == cedula).CountAsync();

            return tramites;
        }

        public async Task<List<int>> TramitesRealizadosPorUsuarioEnLaSemanaAsync(string cedula)
        {
            if (cedula == null)
            {
                return new List<int>();
            }

            var tramitesPorDia = new List<int>();

            DateTime hoyUtc = DateTime.UtcNow.Date;

            int diaDeLaSemana = (int)hoyUtc.DayOfWeek == 0 ? 7 : (int)hoyUtc.DayOfWeek;

            DateTime ultimoLunes = hoyUtc.AddDays(-(diaDeLaSemana - 1));

            for (int i = 0; i < 7; i++)
            {

                DateTime diaActual = ultimoLunes.AddDays(i);
                var cantidadTramites = await _context.TramitesInformacion
                                                    .CountAsync(t => t.UsuarioCreacion == cedula && t.FechaCreacion.Value.Date == diaActual.Date);
                tramitesPorDia.Add(cantidadTramites);

            }

            return tramitesPorDia;
        }

        public async Task<List<int>> TramitesRealizadosPorUsuarioEnElAnioAsync(string cedula)
        {
            if (cedula == null)
            {
                return new List<int>();
            }

            var tramitesPorMes = new List<int>();

            DateTime hoyUtc = DateTime.UtcNow; // Utilizar DateTime.UtcNow en lugar de DateTime.Now

            for (int mes = 1; mes <= 12; mes++)
            {
                DateTime primerDiaDelMes = new DateTime(hoyUtc.Year, mes, 1, 0, 0, 0, DateTimeKind.Utc); // Especificar la zona horaria UTC
                DateTime ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999); // Especificar la zona horaria UTC y ajustar a la última hora del día

                var cantidadTramites = await _context.TramitesInformacion
                    .CountAsync(t => t.UsuarioCreacion == cedula && t.FechaCreacion >= primerDiaDelMes && t.FechaCreacion <= ultimoDiaDelMes);

                tramitesPorMes.Add(cantidadTramites);
            }

            return tramitesPorMes;

        }

        public Task<int> TramitesActivosAsync()
        {
            //Tramites activos en el sistema

            return _context.Tramites.Where(t => t.estaActivo == true).CountAsync();



        }

        public async Task<double> PorcentajeRealizadosPorUsuarioEnLaSemanaAsync(string cedula)
        {
            // Fecha de inicio de la semana actual
            var fechaInicioSemana = DateTime.UtcNow.AddDays(-7);

            // Trámites realizados por el usuario en la semana
            var tramitesUsuarioEnSemana = await _context.TramitesInformacion
                .Where(t => t.UsuarioCreacion == cedula && t.FechaCreacion >= fechaInicioSemana)
                .CountAsync();

            // Total de trámites realizados en la semana por todos los usuarios
            var totalTramitesEnSemana = await _context.TramitesInformacion
                .Where(t => t.FechaCreacion >= fechaInicioSemana)
                .CountAsync();

            // Calcular el porcentaje
            double porcentaje = 0.0;

            if (totalTramitesEnSemana > 0)
            {
                porcentaje = (double)tramitesUsuarioEnSemana / totalTramitesEnSemana * 100;
            }

            return porcentaje;
        }

        public async Task<List<int>> InclusionesAnualesAsync(string cedula)
        {
            var tramitesPorMes = new List<int>();

            DateTime hoyUtc = DateTime.UtcNow;

            for (int mes = 1; mes <= 12; mes++)
            {
                DateTime primerDiaDelMes = new DateTime(hoyUtc.Year, mes, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                var cantidadTramites = await _context.TramitesInformacion
                    .CountAsync(t => t.FechaCreacion >= primerDiaDelMes &&
                                     t.FechaCreacion <= ultimoDiaDelMes &&
                                     (t.TramiteId == 2 || t.TramiteId == 4 || t.TramiteId == 6) &&
                                     t.UsuarioCreacion == cedula);

                tramitesPorMes.Add(cantidadTramites);
            }

            return tramitesPorMes;
        }


        public async Task<List<int>> ExclusionesAnualesAsync(string cedula)
        {
            var tramitesPorMes = new List<int>();

            DateTime hoyUtc = DateTime.UtcNow;

            for (int mes = 1; mes <= 12; mes++)
            {
                DateTime primerDiaDelMes = new DateTime(hoyUtc.Year, mes, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                var cantidadTramites = await _context.TramitesInformacion
                    .CountAsync(t => t.FechaCreacion >= primerDiaDelMes &&
                                     t.FechaCreacion <= ultimoDiaDelMes &&
                                     (t.TramiteId == 3 || t.TramiteId == 5 || t.TramiteId == 7) &&
                                     t.UsuarioCreacion == cedula);

                tramitesPorMes.Add(cantidadTramites);
            }

            return tramitesPorMes;
        }


        public async Task<List<double>> PorcentajeTramitesAsync(string cedula)
        {
            var porcentajeTramites = new List<double>();

            DateTime hoyUtc = DateTime.UtcNow;

            // Obtener el total de trámites para el usuario especificado
            var totalTramitesUsuario = await _context.TramitesInformacion
                .CountAsync(t => t.UsuarioCreacion == cedula && t.TramiteId >= 1 && t.TramiteId <= 7);

            // Calcular la cantidad de trámites para los grupos de trámites 1, 2 y 3, 4 y 5, y 6 y 7
            var cantidadTramitesGrupos = new List<int>
            {
                await _context.TramitesInformacion.CountAsync(t => t.UsuarioCreacion == cedula && t.TramiteId == 1),
                await _context.TramitesInformacion.CountAsync(t => t.UsuarioCreacion == cedula && (t.TramiteId == 2 || t.TramiteId == 3)),
                await _context.TramitesInformacion.CountAsync(t => t.UsuarioCreacion == cedula && (t.TramiteId == 4 || t.TramiteId == 5)),
                await _context.TramitesInformacion.CountAsync(t => t.UsuarioCreacion == cedula && (t.TramiteId == 6 || t.TramiteId == 7))
            };

            // Calcular el porcentaje para cada grupo y agregarlo a la lista
            foreach (var cantidad in cantidadTramitesGrupos)
            {
                double porcentaje = (double)cantidad / totalTramitesUsuario * 100;
                porcentajeTramites.Add(porcentaje);
            }

            return porcentajeTramites;
        }


    }
}

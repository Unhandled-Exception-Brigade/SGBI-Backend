using SGBI.SGBI.Core.DTOs.Dashboard;

namespace SGBI.SGBI.Core.Interfaces
{
    public interface IDashboardService
    {
        Task<int> TramitesRealizadosUltimoDiaAsync(string cedula);

        Task<int> ObtenerCantidadDeTramitesEnElSistemaAsync(string cedula);

        Task<List<int>> TramitesRealizadosPorUsuarioEnLaSemanaAsync(string cedula);

        Task<List<int>> TramitesRealizadosPorUsuarioEnElAnioAsync(string cedula);
        Task<int> TramitesActivosAsync();
        Task<double> PorcentajeRealizadosPorUsuarioEnLaSemanaAsync(string cedula);
        Task<List<int>> InclusionesAnualesAsync(string cedula);
        Task<List<int>> ExclusionesAnualesAsync(string cedula);
        Task<List<double>> PorcentajeTramitesAsync(string cedula);
        Task<int> TramitesInactivosAsync();
    }
}

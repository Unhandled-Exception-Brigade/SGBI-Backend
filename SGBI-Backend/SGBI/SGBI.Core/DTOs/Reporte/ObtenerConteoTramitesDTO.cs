namespace SGBI.SGBI.Core.DTOs.Reporte;

public class ObtenerConteoTramitesDTO
{
    public int? CantidadDeTramitesDiferentes { get; set; } 
    public int? TotalDeTramites { get; set; }

    public List<EstadisticaTramiteDTO>? Estadisticas { get; set;}
}
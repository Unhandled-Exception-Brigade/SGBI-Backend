using Microsoft.Identity.Client;

namespace SGBI.SGBI.Core.DTOs.Reporte;

public class DetalleMesReporteContaduriaDTO
{
    public int IdMes {  get; set; }
    public string? Mes {  get; set; }
    public double ExclusionAnosAnteriores { get; set; }
    public double ExclusionAnoActual { get; set; }
    public double InclusionAnosAnteriores { get; set; }
    public double InclusionAnoActual { get; set; }
}
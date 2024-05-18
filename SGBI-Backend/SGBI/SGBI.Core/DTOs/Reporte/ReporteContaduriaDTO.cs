using Microsoft.Identity.Client;

namespace SGBI.SGBI.Core.DTOs.Reporte;

public class ReporteContaduriaDTO
{
    public double TotalExclusionAnosAnteriores { get; set; }
    public double TotalExclusionAnoActual { get; set; }
    public double TotalInclusionAnosAnteriores { get; set; }
    public double TotalInclusionAnoActual { get; set; }

    public List<DetalleMesReporteContaduriaDTO> detalleMesReporteContaduria { get; set; }
}
namespace SGBI.SGBI.Core.DTOs.Reporte;

public class ReporteExoneracionDTO
{
    public DateTime? Fecha {  get; set; }
    public string DuenoActual {  get; set; }
    public double? MontoExonerarAnoAnteriores { get; set; }
    public double? MontoExonerarAnoActual { get; set; }
}
namespace SGBI.SGBI.Core.DTOs.Reporte;

public class ReporteDatosBasuraDTO
{
    //SPRINT 5
    public bool? Metros { get; set; }
    public bool? Tarifa { get; set; }
    public bool? MontoTotal { get; set; }
    public bool? EstaSiendoIncluido { get; set; } = true;
}
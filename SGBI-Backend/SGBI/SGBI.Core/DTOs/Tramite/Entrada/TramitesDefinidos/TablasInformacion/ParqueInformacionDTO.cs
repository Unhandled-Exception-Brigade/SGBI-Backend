namespace SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;

public class ParqueInformacionDto
{

    public int TramiteInformacionId { get; set; }
    public double MontoTotalAnoActual { get; set; }
    public double MontoTotalAnosAnteriores { get; set; }
    public int CantidadTrimestre { get; set; }
    public bool EstaSiendoIncluido { get; set; }
}
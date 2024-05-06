namespace SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;

public class AseoInformacionDto
{
    public int TramiteInformacionId { get; set; }
    public double MontoTotalAnoActual { get; set; } = 0;
    public double MontoTotalAnosAnteriores { get; set; } = 0;
    public int CantidadTrimestre { get; set; }
    public bool EstaSiendoIncluido { get; set; } = true;
}
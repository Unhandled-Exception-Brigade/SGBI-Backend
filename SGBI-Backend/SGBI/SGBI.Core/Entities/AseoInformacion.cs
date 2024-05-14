namespace SGBI.SBGI.Core.Entities;

public class AseoInformacion
{
    public int Id { get; set; }
    public double MontoTotalAnoActual { get; set; }
    public double MontoTotalAnosAnteriores { get; set; }
    public int CantidadTrimestre { get; set; }
    public bool EstaSiendoIncluido { get; set; }
    public string? NumeroDocumento { get; set; }
    public TramiteInformacion TramiteInformacion { get; set; }
    public int TramiteInformacionId { get; set; }
}
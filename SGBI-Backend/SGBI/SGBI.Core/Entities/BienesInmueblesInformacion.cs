namespace SGBI.SBGI.Core.Entities;

public class BienesInmueblesInformacion
{
    public int Id { get; set; }
    public double MontoTotalAnoActual { get; set; }
    public double MontoTotalAnosAnteriores { get; set; }
    public int CantidadTrimestre { get; set; }
    public bool EstaSiendoIncluido { get; set; }

    public TramiteInformacion TramiteInformacion { get; set; }
    public int TramiteInformacionId { get; set; }
}
namespace SGBI.SBGI.Core.Entities;

public class BasuraInformacion
{

    public int Id { get; set; }
    public double Tarifa {  get; set; }
    public int CantidadTrimestre { get; set; }
    public double MontoAnoActual{ get; set; }
    public double MontoAnoAnteriores { get; set; }
    public bool EstaSiendoIncluido { get; set; }
    public string? NumeroDocumento { get; set; }
    public TramiteInformacion TramiteInformacion { get; set; }
    public int TramiteInformacionId { get; set; }


}
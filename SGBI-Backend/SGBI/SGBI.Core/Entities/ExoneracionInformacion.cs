using System.Drawing;
using System.Threading;

namespace SGBI.SBGI.Core.Entities;

public class ExoneracionInformacion
{
    public int Id { get; set; }
    //public double? MontoExonerar { get; set; }
    public double? MontoExonerarAnoAnteriores { get; set; }
    public double? MontoExonerarAnoActual { get; set; }
    //public double? Excedente { get; set; }
    public int? CantidadTrimestre { get; set; }
    public TramiteInformacion TramiteInformacion { get; set; }
    public int TramiteInformacionId { get; set; }

}
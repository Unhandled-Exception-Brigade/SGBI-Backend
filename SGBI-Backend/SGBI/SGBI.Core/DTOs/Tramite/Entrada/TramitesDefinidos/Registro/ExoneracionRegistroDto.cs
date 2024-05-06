using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;

namespace SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.Registro
{
    public class ExoneracionRegistroDto
    {
        public int TramiteId { get; set; }
        public string? CodigoDepartamento { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? DuenoActual { get; set; }
        public double? ImponibleActual { get; set; }
        public List<string>? FolioReal { get; set; }
        public string? Descripcion { get; set; }
        public ExoneracionInformacionDto? ExoneracionInformacionDTO { get; set; }
    }

}

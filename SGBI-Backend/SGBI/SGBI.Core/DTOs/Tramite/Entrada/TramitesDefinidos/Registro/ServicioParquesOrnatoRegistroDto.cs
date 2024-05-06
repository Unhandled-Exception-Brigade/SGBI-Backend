using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;

namespace SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.Registro
{
    public class ServicioParquesOrnatoRegistroDto
    {
        public int TramiteId { get; set; }
        public string? CodigoDepartamento { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? DuenoActual { get; set; }
        public List<string>? FolioReal { get; set; }
        public double? ImponibleActual { get; set; }
        public string? Descripcion { get; set; }
        public ParqueInformacionDto? ParqueInformacionDTO { get; set; }
    }
}

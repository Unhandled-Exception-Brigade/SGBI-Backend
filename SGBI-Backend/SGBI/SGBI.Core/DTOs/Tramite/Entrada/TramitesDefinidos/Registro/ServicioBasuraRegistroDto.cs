using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;

namespace SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.Registro
{
    public class ServicioBasuraRegistroDto
    {
        public int TramiteId { get; set; }
        public string? CodigoDepartamento { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? DuenoAnterior { get; set; }
        public string? DuenoActual { get; set; }
        public List<string>? FolioReal { get; set; }
        public double? ImponibleAnterior { get; set; }
        public double? ImponibleActual { get; set; }
        public string? Descripcion { get; set; }
        public string? NumeroDocumento { get; set; }
        public BasuraInformacionDto? BasuraInformacionDTO { get; set; }
    }
}

﻿using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;

namespace SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesGenerados
{
    public class TramiteInformacionActualizacionDTO
    {
        public string? CodigoDepartamento { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? DuenoAnterior { get; set; }
        public string? DuenoActual { get; set; }
        public double? ImponibleAnterior { get; set; }
        public double? ImponibleActual { get; set; }
        public List<string>? FolioReal { get; set; }
        public string? FincaMadre { get; set; }
        public string Descripcion { get; set; }

    }
}
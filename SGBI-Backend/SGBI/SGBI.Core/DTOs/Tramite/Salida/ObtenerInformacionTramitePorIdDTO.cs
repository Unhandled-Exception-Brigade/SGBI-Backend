﻿namespace SGBI.SGBI.Core.DTOs.Tramite.Salida
{
    public class ObtenerInformacionTramitePorIdDTO
    {
        public int Id { get; set; }
        public int TramiteId { get; set; }
        public string? CodigoDepartamento { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? DuenoAnterior { get; set; }
        public string? DuenoActual { get; set; }
        public double? ImponibleAnterior { get; set; }
        public double? ImponibleActual { get; set; }
        public List<string>? FolioReal { get; set; }
        public string? FincaMadre { get; set; }
        public string? Descripcion { get; set; }
    }
}
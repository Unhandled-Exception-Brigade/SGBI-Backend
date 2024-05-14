using SGBI.SBGI.Core.Entities;

namespace SGBI.SGBI.Core.Entities
{
    public class CodigoDepartamento: EntidadBase
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGBI.SBGI.Core.Entities;

public class Tarifa : EntidadBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string? Descripcion { get; set; }

    [Required] public double MontoColones { get; set; }
}
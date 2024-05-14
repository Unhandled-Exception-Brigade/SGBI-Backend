using SGBI.SGBI.API.Data;
using SGBI.SGBI.Core.Entities;

namespace SGBI.SGBI.Core.Common.Seeds;

public class SeedCodigoDepartamento
{
    private readonly ApplicationDbContext _context;

    public SeedCodigoDepartamento(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task SeedAsync()
        {
            bool vacio = !_context.CodigoDepartamentos.Any();
            if (vacio)
            {

                var IG = new CodigoDepartamento
                {
                    Codigo = "IG",
                    Descripcion = "Ingeniería",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };

                var CO = new CodigoDepartamento
                {
                    Codigo = "CO",
                    Descripcion = "Cobros",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };
                
                var VU = new CodigoDepartamento
                {
                    Codigo = "VU",
                    Descripcion = "Ventillas",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };
                
                await _context.CodigoDepartamentos.AddAsync(IG);
                await _context.CodigoDepartamentos.AddAsync(CO);
                await _context.CodigoDepartamentos.AddAsync(VU);

                await _context.SaveChangesAsync();
            }
        }
    
}
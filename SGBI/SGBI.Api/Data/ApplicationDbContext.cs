using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.Core.Entities;

namespace SGBI.SGBI.API.Data;

public class ApplicationDbContext : IdentityDbContext<Usuario>
{
    private readonly IUsuarioActualService _usuarioActualService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IUsuarioActualService usuarioActualService) : base(options)
    {
        _usuarioActualService = usuarioActualService;
    }


    public required DbSet<Tarifa> Tarifas { get; set; }


    public DbSet<Tramite> Tramites { get; set; }

    public DbSet<TramiteCampo> TramiteCampos { get; set; }

    public DbSet<TramiteInformacion> TramitesInformacion { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ProcesarSalvado();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ProcesarSalvado()
    {
        var horaActual = DateTime.UtcNow;
        foreach (var item in ChangeTracker.Entries()
                     .Where(e => e.State == EntityState.Added && e.Entity is EntidadBase))
        {
            var entidad = item.Entity as EntidadBase;
            entidad!.FechaCreacion = horaActual;
            entidad.UsuarioCreacion = _usuarioActualService.ObtenerNombreUsuarioActual();
            entidad.FechaModificacion = horaActual;
            entidad.UsuarioModificacion = _usuarioActualService.ObtenerNombreUsuarioActual();
        }

        foreach (var item in ChangeTracker.Entries()
                     .Where(e => e.State == EntityState.Modified && e.Entity is EntidadBase))
        {
            var entidad = item.Entity as EntidadBase;
            entidad!.FechaModificacion = horaActual;
            entidad.UsuarioModificacion = _usuarioActualService.ObtenerNombreUsuarioActual();
            item.Property(nameof(entidad.FechaCreacion)).IsModified = false;
            item.Property(nameof(entidad.UsuarioCreacion)).IsModified = false;
        }
    }
}
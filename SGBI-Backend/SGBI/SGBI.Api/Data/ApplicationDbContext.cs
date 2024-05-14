using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGBI.SBGI.Core.Entities;
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
    public DbSet<AseoInformacion> AseosInformacion { get; set; }
    public DbSet<BasuraInformacion> BasuraInformacion { get; set; }
    public DbSet<ExoneracionInformacion> ExoneracionesInformacion { get; set; }
    public DbSet<ParqueInformacion> ParquesInformacion { get; set; }
    public DbSet<CodigoDepartamento> CodigoDepartamentos { get; set; }
    public DbSet<BienesInmueblesInformacion> BienesInmueblesInformacion { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Tramite>()
            .HasOne(x => x.TramiteCampo)
            .WithOne(x => x.Tramite)
            .HasForeignKey<TramiteCampo>(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TramiteInformacion>()
            .HasOne(x => x.Tramite)
            .WithMany(x => x.TramiteInformacion)
            .HasForeignKey(x => x.TramiteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AseoInformacion>()
            .HasOne(x => x.TramiteInformacion);

        modelBuilder.Entity<BasuraInformacion>()
            .HasOne(x => x.TramiteInformacion);

        modelBuilder.Entity<ExoneracionInformacion>()
            .HasOne(x => x.TramiteInformacion);

        modelBuilder.Entity<BienesInmueblesInformacion>()
            .HasOne(x => x.TramiteInformacion);

        modelBuilder.Entity<ParqueInformacion>()
            .HasOne(x => x.TramiteInformacion);

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
            if (entidad.UsuarioCreacion != "SGBI")
            {
                entidad!.FechaCreacion = horaActual;
                entidad.UsuarioCreacion = _usuarioActualService.ObtenerNombreUsuarioActual();
                entidad.FechaModificacion = horaActual;
                entidad.UsuarioModificacion = _usuarioActualService.ObtenerNombreUsuarioActual();
            }
            else
            {
                entidad!.FechaCreacion = horaActual;
                entidad.UsuarioCreacion = "SGBI";
                entidad.FechaModificacion = horaActual;
                entidad.UsuarioModificacion = "SGBI";
            }
            
        }

        foreach (var item in ChangeTracker.Entries()
                     .Where(e => e.State == EntityState.Modified && e.Entity is EntidadBase))
        {
            var entidad = item.Entity as EntidadBase;

            if (entidad.UsuarioCreacion != "SGBI")
            {
                entidad!.FechaModificacion = horaActual;
                entidad.UsuarioModificacion = _usuarioActualService.ObtenerNombreUsuarioActual();
                item.Property(nameof(entidad.FechaCreacion)).IsModified = false;
                item.Property(nameof(entidad.UsuarioCreacion)).IsModified = false;
            }
            else
            {
                entidad!.FechaCreacion = horaActual;
                entidad.UsuarioCreacion = "SGBI";
                entidad.FechaModificacion = horaActual;
                entidad.UsuarioModificacion = "SGBI";
            }
        }
    }
}
using SGBI.SBGI.Core.Entities;
using SGBI.SGBI.API.Data;

namespace SGBI.SGBI.Core.Common.Seeds
{
    public class SeedTramites
    {
        private readonly ApplicationDbContext _context;

        public SeedTramites(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            bool vacio = !_context.Tramites.Any();
            if (vacio)
            {

                var Exoneracion = new Tramite
                {
                    Nombre = "Exoneración",
                    Codigo = "EXO",
                    Descripcion = "Trámite de exoneración de Bienes Inmuebles",
                    estaActivo = true,
                    esTramitePorDefecto = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };

                var InclusionBasura = new Tramite
                {
                    Nombre = "Inclusión de Mantenimiento de Parques y Obras de Ornato",
                    Codigo = "INCLPO",
                    Descripcion = "Trámite para hacer la inclusión del servicio de Parques y Obras de Ornato en SGBI",
                    estaActivo = true,
                    esTramitePorDefecto = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };

                var ExclusionMantenimientoParquesbrasOrnato = new Tramite
                {
                    Nombre = "Exclusión de Mantenimiento de Parques y Obras de Ornato",
                    Codigo = "EXCLPO",
                    Descripcion = "Trámite para hacer la exclusión del servicio de Parques y Obras de Ornato en SGBI",
                    estaActivo = true,
                    esTramitePorDefecto = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };

                var InclusionServicioRecoleccionDeBasura = new Tramite
                {
                    Nombre = "Inclusión de servicio de Recolección de Basura",
                    Codigo = "INCLBAS",
                    Descripcion = "Tramite para incluir el servicio de recolección de basura de una finca",
                    estaActivo = true,
                    esTramitePorDefecto = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };

                var ExclusionServicioRecoleccionDeBasura = new Tramite
                {
                    Nombre = "Exclusión de servicio de Recolección de Basura",
                    Codigo = "EXCLBAS",
                    Descripcion = "Tramite para excluir el servicio de recolección de basura de una finca",
                    estaActivo = true,
                    esTramitePorDefecto = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };

                var IncldusionAseoViasSitiosPublicos = new Tramite
                {
                    Nombre = "Inclusión de aseo de vías y sitios públicos",
                    Codigo = "INCLASP",
                    Descripcion = "Tramite para incluir el servicio de vias y sitios públicos de una finca",
                    estaActivo = true,
                    esTramitePorDefecto = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };

                var ExclusionAseoViasSitiosPublicos = new Tramite
                {
                    Nombre = "Exclusión de aseo de vías y sitios públicos",
                    Codigo = "EXCLASP",
                    Descripcion = "Tramite para excluir el servicio de vias y sitios públicos de una finca",
                    estaActivo = true,
                    esTramitePorDefecto = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };

                var ExclusionBienesInmuebles = new Tramite
                {
                    Nombre = "Exclusión de Bienes Inmuebles",
                    Codigo = "EXCLBI",
                    Descripcion = "Tramite para excluir los bienes inmuebles",
                    estaActivo = true,
                    esTramitePorDefecto = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };

                var InclusionBienesInmuebles = new Tramite
                {
                    Nombre = "Inclusion de Bienes Inmuebles",
                    Codigo = "INCLBI",
                    Descripcion = "Tramite para incluir los bienes inmuebles",
                    estaActivo = true,
                    esTramitePorDefecto = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SGBI",
                    UsuarioModificacion = "SGBI"
                };

                await _context.Tramites.AddAsync(Exoneracion);
                await _context.Tramites.AddAsync(InclusionBasura);
                await _context.Tramites.AddAsync(ExclusionMantenimientoParquesbrasOrnato);
                await _context.Tramites.AddAsync(InclusionServicioRecoleccionDeBasura);
                await _context.Tramites.AddAsync(ExclusionServicioRecoleccionDeBasura);
                await _context.Tramites.AddAsync(IncldusionAseoViasSitiosPublicos);
                await _context.Tramites.AddAsync(ExclusionAseoViasSitiosPublicos);
                await _context.Tramites.AddAsync(ExclusionBienesInmuebles);
                await _context.Tramites.AddAsync(InclusionBienesInmuebles);

                await _context.SaveChangesAsync();
            }
        }
    }

}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGBI.SBGI.Core.Entities;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.API.Data;

using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using OfficeOpenXml;
using SGBI.SGBI.Core.DTOs.Tramite;
using SGBI.SGBI.Core.DTOs.Reporte;
using System.Globalization;

namespace SGBI.SGBI.API.Services;

public class ReporteService : IReporteService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ReporteService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }



    public byte[] GenerarPDFDeUsuarios()
    {
        var tableData = _context.Users.ToList();

        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Arial", 12, XFontStyle.Regular);

        var yPosition = 40;
        foreach (var row in tableData)
        {
            var rowData = $"USUARIO: {row.UserName}, {row.Nombre}"; // Adjust according to your table structure
            gfx.DrawString(rowData, font, XBrushes.Black, new XPoint(40, yPosition));
            yPosition += 20; // Adjust spacing as needed
        }

        // Save the PDF document to a byte array
        using (var memoryStream = new MemoryStream())
        {
            document.Save(memoryStream, false);
            return memoryStream.ToArray();
        }
    }


    public byte[] GenerarExcelDeUsuarios()
    {
        var tableData = _context.Users.ToList();

        // Create a new Excel package
        using (var excelPackage = new ExcelPackage())
        {
            // Add a new worksheet
            var worksheet = excelPackage.Workbook.Worksheets.Add("Usuarios");

            // Add headers
            worksheet.Cells[1, 1].Value = "Usuario";
            worksheet.Cells[1, 2].Value = "Nombre";

            // Populate data
            int row = 2;
            foreach (var item in tableData)
            {
                worksheet.Cells[row, 1].Value = item.UserName;
                worksheet.Cells[row, 2].Value = item.Nombre;
                row++;
            }

            // Auto-fit columns
            worksheet.Cells.AutoFitColumns();

            // Convert Excel package to byte array
            return excelPackage.GetAsByteArray();
        }
    }
    public async Task<ObtenerConteoTramitesDTO> ObtenerConteoTramitesAsync(DateTime? FechaInicio, DateTime? FechaFinal, bool? SoloRolSeleccionado, string? Rol, string? Usuario)
    {
        try
        {
            ObtenerConteoTramitesDTO ConteoTramiteDto = new ObtenerConteoTramitesDTO();

            IQueryable<TramiteInformacion> tramitesInformacionQuery = _context.TramitesInformacion.OrderBy(x => x.Id);
            IQueryable<Tramite> tramitesQuery = _context.Tramites.OrderBy(x => x.Id);
            

            if (FechaInicio != null)
            {
                tramitesInformacionQuery = tramitesInformacionQuery.Where(x => x.FechaCreacion >= FechaInicio);
            }

            if (FechaFinal != null)
            {
                tramitesInformacionQuery = tramitesInformacionQuery.Where(x => x.FechaCreacion <= FechaFinal);
            }

            if(SoloRolSeleccionado != null && Usuario == null)
            {

                string FiltroRol = "DEPURACION";

                if(Rol != null)
                {
                    FiltroRol = Rol.ToUpper();
                }

                if (SoloRolSeleccionado == true)
                {

                    tramitesInformacionQuery = tramitesInformacionQuery
                    .Join(_context.Users, tramite => tramite.UsuarioCreacion, user => user.UserName,
                          (tramite, user) => new { Tramite = tramite, User = user })
                    .Join(_context.UserRoles, tu => tu.User.Id, userRole => userRole.UserId,
                          (tu, userRole) => new { tu.Tramite, tu.User, UserRole = userRole })
                    .Join(_context.Roles, tuur => tuur.UserRole.RoleId, role => role.Id,
                          (tuur, role) => new { tuur.Tramite, tuur.User, tuur.UserRole, Role = role })
                    .Where(tuur => tuur.Role.NormalizedName == FiltroRol)
                    .Select(tuur => tuur.Tramite);


                }
                else
                {
                    tramitesInformacionQuery = tramitesInformacionQuery
                    .Join(_context.Users, tramite => tramite.UsuarioCreacion, user => user.UserName,
                          (tramite, user) => new { Tramite = tramite, User = user })
                    .Join(_context.UserRoles, tu => tu.User.Id, userRole => userRole.UserId,
                          (tu, userRole) => new { tu.Tramite, tu.User, UserRole = userRole })
                    .Join(_context.Roles, tuur => tuur.UserRole.RoleId, role => role.Id,
                          (tuur, role) => new { tuur.Tramite, tuur.User, tuur.UserRole, Role = role })
                    .Where(tuur => tuur.Role.NormalizedName != FiltroRol)
                    .Select(tuur => tuur.Tramite);
                }
            }
            
            if (Usuario != null)
            {
                tramitesInformacionQuery = _context.TramitesInformacion.OrderBy(x => x.UsuarioCreacion == Usuario);
            }

            int totalTramitesCount = await tramitesQuery.CountAsync();

            int totalTramitesInformacionCount = await tramitesInformacionQuery.CountAsync();

            ConteoTramiteDto.CantidadDeTramitesDiferentes = totalTramitesCount;
            ConteoTramiteDto.TotalDeTramites = totalTramitesInformacionCount;

            List<Tramite> tramites = await tramitesQuery.ToListAsync();

            foreach (var tramite in tramites)
            {
                EstadisticaTramiteDTO EstadisticaTramiteDto = new EstadisticaTramiteDTO();
                EstadisticaTramiteDto.CodigoTramite = tramite.Codigo;
                EstadisticaTramiteDto.NombreTramite = tramite.Nombre;

                int conteoTramite = await tramitesInformacionQuery
                    .Where(x => x.TramiteId == tramite.Id)
                    .CountAsync();

                EstadisticaTramiteDto.ConteoTramite = conteoTramite;

                EstadisticaTramiteDto.PorcentajeTramite = totalTramitesInformacionCount == 0 ? 0 :
                   Math.Round((double)(EstadisticaTramiteDto.ConteoTramite * 100) / totalTramitesInformacionCount, 3);

                if (ConteoTramiteDto.Estadisticas == null)
                {
                    ConteoTramiteDto.Estadisticas = new List<EstadisticaTramiteDTO>();
                }

                ConteoTramiteDto.Estadisticas.Add(EstadisticaTramiteDto);
            }

            if (ConteoTramiteDto.Estadisticas != null)
            {
                ConteoTramiteDto.Estadisticas = ConteoTramiteDto.Estadisticas.OrderByDescending(x => x.ConteoTramite).ToList();
            }

            return ConteoTramiteDto;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in ObtenerConteoTramitesAsync: " + ex.Message);
            throw;
        }
    }

    public async Task<List<DetalleMesReporteContaduriaDTO>> ObtenerReporteContaduriaBienesInmueblesAsync(int Ano, bool? SoloRolSeleccionado, string? Rol)
    {

        var detalleMeses = new List<DetalleMesReporteContaduriaDTO>();
        for (int month = 1; month <= 12; month++)
        {
            detalleMeses.Add(new DetalleMesReporteContaduriaDTO
            {
                IdMes = month,
                Mes = CultureInfo.GetCultureInfo("es-ES").DateTimeFormat.GetMonthName(month),
                InclusionAnoActual = 0.000,
                InclusionAnosAnteriores = 0.000,
                ExclusionAnoActual = 0.000,
                ExclusionAnosAnteriores = 0.000
            });
        }

        try
        {
            
            var bienesInmuebles = await (from bien in _context.BienesInmueblesInformacion
                                         join tramite in _context.TramitesInformacion
                                         on bien.TramiteInformacionId equals tramite.Id
                                         where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                         select new { bien, tramite })
                                         .ToListAsync();

            if(SoloRolSeleccionado != null)
            {
                string RolParaFiltrar = "DEPURACION";

                if(Rol != null)
                {
                    RolParaFiltrar = Rol.ToUpper();
                }

                if(SoloRolSeleccionado == true)
                {
                    bienesInmuebles = await (from bien in _context.BienesInmueblesInformacion
                                                 join tramite in _context.TramitesInformacion
                                                 on bien.TramiteInformacionId equals tramite.Id
                                                 join user in _context.Users
                                                 on tramite.UsuarioCreacion equals user.UserName
                                                 join userRole in _context.UserRoles
                                                 on user.Id equals userRole.UserId
                                                 join role in _context.Roles
                                                 on userRole.RoleId equals role.Id
                                                 where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                                 && role.NormalizedName == RolParaFiltrar select new { bien, tramite }).ToListAsync();


                }
                else
                {
                    bienesInmuebles = await (from bien in _context.BienesInmueblesInformacion
                                             join tramite in _context.TramitesInformacion
                                             on bien.TramiteInformacionId equals tramite.Id
                                             join user in _context.Users
                                             on tramite.UsuarioCreacion equals user.UserName
                                             join userRole in _context.UserRoles
                                             on user.Id equals userRole.UserId
                                             join role in _context.Roles
                                             on userRole.RoleId equals role.Id
                                             where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                             && role.NormalizedName != RolParaFiltrar
                                             select new { bien, tramite }).ToListAsync();
                }
            }

            foreach (var item in bienesInmuebles)
            {
                var bien = item.bien;
                var tramite = item.tramite;

                var detalleMes = detalleMeses.FirstOrDefault(d => d.IdMes == tramite.FechaCreacion.Value.Month);
                if (detalleMes != null)
                {
                    if (bien.EstaSiendoIncluido)
                    {
                        if (bien.MontoTotalAnoActual != null)
                        {
                            detalleMes.InclusionAnoActual += bien.MontoTotalAnoActual;
                            detalleMes.InclusionAnoActual = Math.Round(detalleMes.InclusionAnoActual, 3);
                        }
                        if (bien.MontoTotalAnosAnteriores != null)
                        {
                            detalleMes.InclusionAnosAnteriores += bien.MontoTotalAnosAnteriores;
                            detalleMes.InclusionAnosAnteriores = Math.Round(detalleMes.InclusionAnosAnteriores, 3);
                        }

                    }
                    else
                    {
                        if (bien.MontoTotalAnoActual != null)
                        {
                            detalleMes.ExclusionAnoActual += bien.MontoTotalAnoActual;
                            detalleMes.ExclusionAnoActual = Math.Round(detalleMes.ExclusionAnoActual, 3);
                        } 
                        if (bien.MontoTotalAnosAnteriores != null)
                        {
                            detalleMes.ExclusionAnosAnteriores += bien.MontoTotalAnosAnteriores;
                            detalleMes.ExclusionAnosAnteriores = Math.Round(detalleMes.ExclusionAnosAnteriores, 3);
                        }
                    }
                }
            }

        }

        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error: " + e.Message, e);
        }

        return detalleMeses;
    }

    public async Task<List<DetalleMesReporteContaduriaDTO>> ObtenerReporteContaduriaBasuraResidencialAsync(int Ano, bool? SoloRolSeleccionado, string? Rol)
    {
        var detalleMeses = new List<DetalleMesReporteContaduriaDTO>();
        for (int month = 1; month <= 12; month++)
        {
            detalleMeses.Add(new DetalleMesReporteContaduriaDTO
            {
                IdMes = month,
                Mes = CultureInfo.GetCultureInfo("es-ES").DateTimeFormat.GetMonthName(month),
                InclusionAnoActual = 0.000, 
                InclusionAnosAnteriores = 0.000, 
                ExclusionAnoActual = 0.000, 
                ExclusionAnosAnteriores = 0.000
            });
        }

        try
        {
            var basuraResidencial = await (from basura in _context.BasuraInformacion
                                         join tramite in _context.TramitesInformacion
                                         on basura.TramiteInformacionId equals tramite.Id
                                         where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                         select new { basura, tramite })
                                         .ToListAsync();

            if (SoloRolSeleccionado != null)
            {
                string RolParaFiltrar = "DEPURACION";

                if (Rol != null)
                {
                    RolParaFiltrar = Rol.ToUpper();
                }

                if (SoloRolSeleccionado == true)
                {
                    basuraResidencial = await (from basura in _context.BasuraInformacion
                                             join tramite in _context.TramitesInformacion
                                             on basura.TramiteInformacionId equals tramite.Id
                                             join user in _context.Users
                                             on tramite.UsuarioCreacion equals user.UserName
                                             join userRole in _context.UserRoles
                                             on user.Id equals userRole.UserId
                                             join role in _context.Roles
                                             on userRole.RoleId equals role.Id
                                             where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                             && role.NormalizedName == RolParaFiltrar
                                             select new { basura, tramite }).ToListAsync();

                }
                else
                {
                    basuraResidencial = await (from basura in _context.BasuraInformacion
                                               join tramite in _context.TramitesInformacion
                                               on basura.TramiteInformacionId equals tramite.Id
                                               join user in _context.Users
                                               on tramite.UsuarioCreacion equals user.UserName
                                               join userRole in _context.UserRoles
                                               on user.Id equals userRole.UserId
                                               join role in _context.Roles
                                               on userRole.RoleId equals role.Id
                                               where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                               && role.NormalizedName != RolParaFiltrar
                                               select new { basura, tramite }).ToListAsync();
                }
            }

            foreach (var item in basuraResidencial)
            {
                var basura = item.basura;
                var tramite = item.tramite;

                var detalleMes = detalleMeses.FirstOrDefault(d => d.IdMes == tramite.FechaCreacion.Value.Month);
                if (detalleMes != null)
                {
                    if (basura.EstaSiendoIncluido)
                    {
                        if (basura.MontoAnoActual != null)
                        {
                            detalleMes.InclusionAnoActual += basura.MontoAnoActual;
                            detalleMes.InclusionAnoActual = Math.Round(detalleMes.InclusionAnoActual, 3);

                        }
                        if (basura.MontoAnoAnteriores != null)
                        {
                            detalleMes.InclusionAnosAnteriores += basura.MontoAnoAnteriores;
                            detalleMes.InclusionAnosAnteriores = Math.Round(detalleMes.InclusionAnosAnteriores, 3);
                        }

                    }
                    else
                    {
                        if (basura.MontoAnoActual != null)
                        {
                            detalleMes.ExclusionAnoActual += basura.MontoAnoActual;
                            detalleMes.ExclusionAnoActual = Math.Round(detalleMes.ExclusionAnoActual, 3);
                        }
                        if (basura.MontoAnoAnteriores != null)
                        {
                            detalleMes.ExclusionAnosAnteriores += basura.MontoAnoAnteriores;
                            detalleMes.ExclusionAnosAnteriores = Math.Round(detalleMes.ExclusionAnosAnteriores, 3);
                        }
                    }
                }
            }

        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error: " + e.Message, e);
        }

        return detalleMeses;
    }

    public async Task<List<DetalleMesReporteContaduriaDTO>> ObtenerReporteContaduriaAseoViasAsync(int Ano, bool? SoloRolSeleccionado, string? Rol)
    {
        var detalleMeses = new List<DetalleMesReporteContaduriaDTO>();
        for (int month = 1; month <= 12; month++)
        {
            detalleMeses.Add(new DetalleMesReporteContaduriaDTO
            {
                IdMes = month,
                Mes = CultureInfo.GetCultureInfo("es-ES").DateTimeFormat.GetMonthName(month),
                InclusionAnoActual = 0,
                InclusionAnosAnteriores = 0,
                ExclusionAnoActual = 0,
                ExclusionAnosAnteriores = 0
            });
        }

        try
        {
            var aseoVias = await (from aseo in _context.AseosInformacion
                                           join tramite in _context.TramitesInformacion
                                           on aseo.TramiteInformacionId equals tramite.Id
                                           where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                           select new { aseo, tramite })
                                         .ToListAsync();


            if (SoloRolSeleccionado != null)
            {
                string RolParaFiltrar = "DEPURACION";

                if (Rol != null)
                {
                    RolParaFiltrar = Rol.ToUpper();
                }

                if (SoloRolSeleccionado == true)
                {
                    aseoVias = await (from aseo in _context.AseosInformacion
                                               join tramite in _context.TramitesInformacion
                                               on aseo.TramiteInformacionId equals tramite.Id
                                               join user in _context.Users
                                               on tramite.UsuarioCreacion equals user.UserName
                                               join userRole in _context.UserRoles
                                               on user.Id equals userRole.UserId
                                               join role in _context.Roles
                                               on userRole.RoleId equals role.Id
                                               where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                               && role.NormalizedName == RolParaFiltrar
                                               select new { aseo, tramite }).ToListAsync();

                }
                else
                {
                    aseoVias = await (from aseo in _context.AseosInformacion
                                      join tramite in _context.TramitesInformacion
                                      on aseo.TramiteInformacionId equals tramite.Id
                                      join user in _context.Users
                                      on tramite.UsuarioCreacion equals user.UserName
                                      join userRole in _context.UserRoles
                                      on user.Id equals userRole.UserId
                                      join role in _context.Roles
                                      on userRole.RoleId equals role.Id
                                      where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                      && role.NormalizedName != RolParaFiltrar
                                      select new { aseo, tramite }).ToListAsync();
                }
            }

            foreach (var item in aseoVias)
            {
                var aseo = item.aseo;
                var tramite = item.tramite;

                var detalleMes = detalleMeses.FirstOrDefault(d => d.IdMes == tramite.FechaCreacion.Value.Month);
                if (detalleMes != null)
                {
                    if (aseo.EstaSiendoIncluido)
                    {
                        if (aseo.MontoTotalAnoActual != null)
                        {
                            detalleMes.InclusionAnoActual += aseo.MontoTotalAnoActual;
                            detalleMes.InclusionAnoActual = Math.Round(detalleMes.InclusionAnoActual, 3);
                        }
                        if (aseo.MontoTotalAnosAnteriores != null)
                        {
                            detalleMes.InclusionAnosAnteriores += aseo.MontoTotalAnosAnteriores;
                            detalleMes.InclusionAnosAnteriores = Math.Round(detalleMes.InclusionAnosAnteriores, 3);
                        }

                    }
                    else
                    {
                        if (aseo.MontoTotalAnoActual != null)
                        {
                            detalleMes.ExclusionAnoActual += aseo.MontoTotalAnoActual;
                            detalleMes.ExclusionAnoActual = Math.Round(detalleMes.ExclusionAnoActual, 3);
                        }
                        if (aseo.MontoTotalAnosAnteriores != null)
                        {
                            detalleMes.ExclusionAnosAnteriores += aseo.MontoTotalAnosAnteriores;
                            detalleMes.ExclusionAnosAnteriores = Math.Round(detalleMes.ExclusionAnosAnteriores, 3);
                        }
                    }
                }
            }

        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error: " + e.Message, e);
        }

        return detalleMeses;
    }

    public async Task<List<DetalleMesReporteContaduriaDTO>> ObtenerReporteContaduriaMantenimientoParqueAsync(int Ano, bool? SoloRolSeleccionado, string? Rol)
    {
        var detalleMeses = new List<DetalleMesReporteContaduriaDTO>();
        for (int month = 1; month <= 12; month++)
        {
            detalleMeses.Add(new DetalleMesReporteContaduriaDTO
            {
                IdMes = month,
                Mes = CultureInfo.GetCultureInfo("es-ES").DateTimeFormat.GetMonthName(month),
                InclusionAnoActual = 0,
                InclusionAnosAnteriores = 0,
                ExclusionAnoActual = 0,
                ExclusionAnosAnteriores = 0
            });
        }

        try
        {
            var mantenimientoParque = await (from parque in _context.ParquesInformacion
                                  join tramite in _context.TramitesInformacion
                                  on parque.TramiteInformacionId equals tramite.Id
                                  where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                  select new { parque, tramite })
                                         .ToListAsync();

            if (SoloRolSeleccionado != null)
            {
                string RolParaFiltrar = "DEPURACION";

                if (Rol != null)
                {
                    RolParaFiltrar = Rol.ToUpper();
                }

                if (SoloRolSeleccionado == true)
                {
                    mantenimientoParque = await (from parque in _context.ParquesInformacion
                                      join tramite in _context.TramitesInformacion
                                      on parque.TramiteInformacionId equals tramite.Id
                                      join user in _context.Users
                                      on tramite.UsuarioCreacion equals user.UserName
                                      join userRole in _context.UserRoles
                                      on user.Id equals userRole.UserId
                                      join role in _context.Roles
                                      on userRole.RoleId equals role.Id
                                      where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                      && role.NormalizedName == RolParaFiltrar
                                      select new { parque, tramite }).ToListAsync();

                }
                else
                {
                    mantenimientoParque = await (from parque in _context.ParquesInformacion
                                                 join tramite in _context.TramitesInformacion
                                                 on parque.TramiteInformacionId equals tramite.Id
                                                 join user in _context.Users
                                                 on tramite.UsuarioCreacion equals user.UserName
                                                 join userRole in _context.UserRoles
                                                 on user.Id equals userRole.UserId
                                                 join role in _context.Roles
                                                 on userRole.RoleId equals role.Id
                                                 where tramite.FechaCreacion.HasValue && tramite.FechaCreacion.Value.Year == Ano
                                                 && role.NormalizedName != RolParaFiltrar
                                                 select new { parque, tramite }).ToListAsync();
                }
            }


            foreach (var item in mantenimientoParque)
            {
                var parque = item.parque;
                var tramite = item.tramite;

                var detalleMes = detalleMeses.FirstOrDefault(d => d.IdMes == tramite.FechaCreacion.Value.Month);
                if (detalleMes != null)
                {
                    if (parque.EstaSiendoIncluido)
                    {
                        if (parque.MontoTotalAnoActual != null)
                        {
                            detalleMes.InclusionAnoActual += parque.MontoTotalAnoActual;
                            detalleMes.InclusionAnoActual = Math.Round(detalleMes.InclusionAnoActual, 3);
                        }
                        if (parque.MontoTotalAnosAnteriores != null)
                        {
                            detalleMes.InclusionAnosAnteriores += parque.MontoTotalAnosAnteriores;
                            detalleMes.InclusionAnosAnteriores = Math.Round(detalleMes.InclusionAnosAnteriores, 3);
                        }

                    }
                    else
                    {
                        if (parque.MontoTotalAnoActual != null)
                        {
                            detalleMes.ExclusionAnoActual += parque.MontoTotalAnoActual;
                            detalleMes.InclusionAnosAnteriores = Math.Round(detalleMes.InclusionAnosAnteriores, 3);
                        }
                        if (parque.MontoTotalAnosAnteriores != null)
                        {
                            detalleMes.ExclusionAnosAnteriores += parque.MontoTotalAnosAnteriores;
                            detalleMes.ExclusionAnosAnteriores = Math.Round(detalleMes.ExclusionAnosAnteriores, 3);
                        }
                    }
                }
            }

        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error: " + e.Message, e);
        }

        return detalleMeses;
    }
}
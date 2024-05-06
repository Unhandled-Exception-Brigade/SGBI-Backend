using Microsoft.AspNetCore.Identity;
using SGBI.SBGI.Core.Common.Helpers;
using SGBI.SBGI.Core.Entities;

namespace SGBI.SGBI.Core.Common.Seeds;

public class SeedUsuarios
{
    private readonly UserManager<Usuario> _userManager;

    public SeedUsuarios(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    public async Task SeedAsync()
    {
        if (!_userManager.Users.Any())
        {
            var william = new Usuario
            {
                Email = "deishuu666@gmail.com",
                Nombre = "William",
                PrimerApellido = "Rodriguez",
                SegundoApellido = "Rocha",
                UserName = "208450864",
                Activo = true,
                EmailConfirmed = true
            };
            
            var alvaro = new Usuario
            {
                Email = "alvaro.herrera.chaves@est.una.ac.cr",
                Nombre = "Alvaro",
                PrimerApellido = "Herrera",
                SegundoApellido = "Chaves",
                UserName = "118590567",
                Activo = true,
                EmailConfirmed = true
            };
            var joshua = new Usuario
            {
                Email = "joshua.cespedes.cedeno@est.una.ac.cr",
                Nombre = "Joshua",
                PrimerApellido = "Cespedes",
                SegundoApellido = "Cedeno",
                UserName = "117990050",
                Activo = true,
                EmailConfirmed = true
            };
            var luis = new Usuario
            {
                Email = "luis.aguilar.bolanos@est.una.ac.cr",
                Nombre = "Luis",
                PrimerApellido = "Aguilar",
                SegundoApellido = "Bolanos",
                UserName = "208470264",
                Activo = true,
                EmailConfirmed = true
            };
            var farlem = new Usuario
            {
                Email = "farlem.vega.campos@est.una.ac.cr",
                Nombre = "Farlem",
                PrimerApellido = "Vega",
                SegundoApellido = "Campos",
                UserName = "208380312",
                Activo = true,
                EmailConfirmed = true
            };
            var michael = new Usuario
            {
                Email = "michael.alfaro.vargas@est.una.ac.cr",
                Nombre = "Michael",
                PrimerApellido = "Alfaro",
                SegundoApellido = "Vargas",
                UserName = "208370435",
                Activo = true,
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(william);
            await _userManager.CreateAsync(alvaro);
            await _userManager.CreateAsync(joshua);
            await _userManager.CreateAsync(luis);
            await _userManager.CreateAsync(farlem);
            await _userManager.CreateAsync(michael);

            const string nuevaContrasena = "Cherry666@";
            const string nuevaContrasena1 = "Admin12345@";

            william.PasswordHash = PasswordHasher.HashPassword(nuevaContrasena!);
            await _userManager.UpdateAsync(william);
            
            alvaro.PasswordHash = PasswordHasher.HashPassword(nuevaContrasena1!);
            await _userManager.UpdateAsync(alvaro);
            
            joshua.PasswordHash = PasswordHasher.HashPassword(nuevaContrasena1!);
            await _userManager.UpdateAsync(joshua);
            
            luis.PasswordHash = PasswordHasher.HashPassword(nuevaContrasena1!);
            await _userManager.UpdateAsync(luis);
            
            farlem.PasswordHash = PasswordHasher.HashPassword(nuevaContrasena1!);
            await _userManager.UpdateAsync(farlem);
            
            michael.PasswordHash = PasswordHasher.HashPassword(nuevaContrasena1!);
            await _userManager.UpdateAsync(michael);
            
            
            await _userManager.AddToRoleAsync(william, "Administrador");
            
            await _userManager.AddToRoleAsync(alvaro, "Administrador");
            
            await _userManager.AddToRoleAsync(joshua, "Administrador");
            
            await _userManager.AddToRoleAsync(luis, "Administrador");
            
            await _userManager.AddToRoleAsync(farlem, "Jefe");
            
            await _userManager.AddToRoleAsync(michael, "Usuario");
            
            
        }
    }
}
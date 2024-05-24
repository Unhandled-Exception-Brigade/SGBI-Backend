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
            var admin = new Usuario
            {
                Email = "vchaves@atenasmuni.go.cr",
                Nombre = "Vinicio",
                PrimerApellido = "Chaves",
                SegundoApellido = "Vargas",
                UserName = "113360999",
                Activo = true,
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(admin);

            const string nuevaContrasena1 = "Admin12345@";

            admin.PasswordHash = PasswordHasher.HashPassword(nuevaContrasena1);
            await _userManager.UpdateAsync(admin);
            
            await _userManager.AddToRoleAsync(admin, "Administrador");
            
        }
    }
}
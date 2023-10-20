using Microsoft.AspNetCore.Identity;

namespace SGBI.SGBI.Core.Common.Seeds;

public class SeedRoles
{
    
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        if (!_roleManager.Roles.Any())
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "Administrador" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "Jefe" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "Usuario" });
        }
    }
    
}
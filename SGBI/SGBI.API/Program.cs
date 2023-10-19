using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SGBI.SBGI.Core.Common.Helpers;
using SGBI.SBGI.Core.Entities;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.API.Data;
using SGBI.SGBI.API.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Especifica la ubicación del archivo de configuración
builder.Configuration.AddJsonFile("SGBI.API/appsettings.json", optional: false, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<Usuario, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Adding Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
// Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
        };
    });

builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy", corsPolicyBuilder =>
        corsPolicyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<ICorreoService, CorreoService>();
builder.Services.AddScoped<IUsuarioActualService, UsuarioActualService>();
builder.Services.AddScoped<ITarifaService, TarifaService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

await SeedData();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


async Task SeedData()
{
    var scopeFactory = app!.Services.GetRequiredService<IServiceScopeFactory>();
    using var scope = scopeFactory.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    context.Database.EnsureCreated();

    if (!roleManager.Roles.Any())
    {
        logger.LogInformation("Creando roles por defecto");

        await roleManager.CreateAsync(new IdentityRole
        {
            Name = "Administrador"
        });

        await roleManager.CreateAsync(new IdentityRole
        {
            Name = "Jefe"
        });

        await roleManager.CreateAsync(new IdentityRole
        {
            Name = "Usuario"
        });
    }

    if (!userManager.Users.Any())
    {
        logger.LogInformation("Creando usuario por defecto");

        var newUser = new Usuario
        {
            Email = "deishuu666@gmail.com",
            Nombre = "William",
            PrimerApellido = "Rodriguez",
            SegundoApellido = "Rocha",
            UserName = "208450864"
        };

        await userManager.CreateAsync(newUser);

        const string nuevaContrasena = "Cherry666@";

        newUser.PasswordHash = PasswordHasher.HashPassword(nuevaContrasena!);
        await userManager.UpdateAsync(newUser);

        await userManager.AddToRoleAsync(newUser, "Administrador");
    }
}
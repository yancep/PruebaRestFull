using Application;
using Identity;
using Identity.Models;
using Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Persistence.Context;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationLayer();
builder.Services.AddApiVersioningExtensions();

builder.Services.AddPersistenceInfraestructure(builder.Configuration);
builder.Services.AddIdentityInfraestructure(builder.Configuration);
// Agregar Identity Services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.useErrorHandlingMiddleware();

app.MapControllers();

// Sembrar datos iniciales
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);
        await DefaultBasicUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        // Manejo de excepciones
        throw;
    }
}

app.Run();

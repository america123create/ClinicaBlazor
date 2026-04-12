using ClinicaBlazor.Components;
using ClinicaBlazor.Data;
using Microsoft.EntityFrameworkCore;
using ClinicaBlazor.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ClinicaBlazor.Models;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<SesionService>();
builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ModuloService>();
builder.Services.AddScoped<PermisoPerfilService>();
builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<PermisoPerfilService>();
builder.Services.Configure<ReCaptchaSettings>(
    builder.Configuration.GetSection("ReCaptcha"));

builder.Services.AddHttpClient<ReCaptchaService>();

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped<JwtService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/error");
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

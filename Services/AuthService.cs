using ClinicaBlazor.Data;
using ClinicaBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaBlazor.Services
{
    public class AuthService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public AuthService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<(Usuario? usuario, string mensaje)> LoginAsync(string username, string password)
        {
            using var context = _dbFactory.CreateDbContext();

            string userLimpio = username.Trim();
            string passLimpia = password.Trim();

            var usuario = await context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == userLimpio);

            if (usuario == null)
                return (null, "No se encontró el username.");

            if (!usuario.Activo)
                return (null, "El usuario está inactivo.");

            if ((usuario.Password ?? string.Empty).Trim() != passLimpia)
                return (null, "La contraseña no coincide.");

            return (usuario, "Login correcto.");
        }
    }
}
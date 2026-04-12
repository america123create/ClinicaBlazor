using ClinicaBlazor.Data;
using ClinicaBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaBlazor.Services
{
    public class PermisoPerfilService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public PermisoPerfilService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public async Task<ClinicaBlazor.Models.PermisoPerfil?> ObtenerPermisoPorPerfilYRutaAsync(int perfilId, string ruta)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.PermisosPerfil
                .Include(p => p.Modulo)
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.PerfilId == perfilId &&
                    p.Modulo != null &&
                    p.Modulo.Ruta == ruta);
        }

        public async Task<List<PermisoPerfil>> ObtenerTodosAsync()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.PermisosPerfil
                .Include(p => p.Perfil)
                .Include(p => p.Modulo)
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task GuardarAsync(PermisoPerfil permiso)
        {
            using var context = _dbFactory.CreateDbContext();

            if (permiso.Id == 0)
            {
                context.PermisosPerfil.Add(permiso);
            }
            else
            {
                context.PermisosPerfil.Update(permiso);
            }

            await context.SaveChangesAsync();
        }
        public async Task<List<ClinicaBlazor.Models.PermisoPerfil>> ObtenerPorPerfilIdAsync(int perfilId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.PermisosPerfil
                .Include(p => p.Modulo)
                .AsNoTracking()
                .Where(p => p.PerfilId == perfilId)
                .ToListAsync();
        }

        public async Task EliminarAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();

            var permiso = await context.PermisosPerfil.FindAsync(id);
            if (permiso == null)
            {
                return;
            }

            context.PermisosPerfil.Remove(permiso);
            await context.SaveChangesAsync();
        }

        public async Task<bool> TieneAccesoConsultaAsync(string nombrePerfil, string ruta)
        {
            using var context = _dbFactory.CreateDbContext();

            var perfil = await context.Perfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Nombre == nombrePerfil);

            if (perfil == null)
                return false;

            var permiso = await context.PermisosPerfil
                .Include(p => p.Modulo)
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.PerfilId == perfil.Id &&
                    p.Modulo != null &&
                    p.Modulo.Ruta == ruta);

            return permiso != null && permiso.Consultar;
        }
    }
}


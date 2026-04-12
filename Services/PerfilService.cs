using ClinicaBlazor.Data;
using ClinicaBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaBlazor.Services
{
    public class PerfilService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public PerfilService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Perfil>> ObtenerTodosAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Perfiles
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }
        public async Task<ClinicaBlazor.Models.Perfil?> ObtenerPorNombreAsync(string nombre)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Perfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Nombre == nombre);
        }
        public async Task GuardarAsync(Perfil perfil)
        {
            using var context = _dbFactory.CreateDbContext();

            if (perfil.Id == 0)
                context.Perfiles.Add(perfil);
            else
                context.Perfiles.Update(perfil);

            await context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();

            var perfil = await context.Perfiles.FindAsync(id);
            if (perfil == null) return;

            context.Perfiles.Remove(perfil);
            await context.SaveChangesAsync();
        }
    }
}
using ClinicaBlazor.Data;
using ClinicaBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaBlazor.Services
{
    public class ModuloService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public ModuloService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Modulo>> ObtenerTodosAsync()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Modulos
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .ToListAsync();
        }

        public async Task GuardarAsync(Modulo modulo)
        {
            using var context = _dbFactory.CreateDbContext();

            if (modulo.Id == 0)
            {
                context.Modulos.Add(modulo);
            }
            else
            {
                context.Modulos.Update(modulo);
            }

            await context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();

            var modulo = await context.Modulos.FindAsync(id);
            if (modulo == null)
            {
                return;
            }

            context.Modulos.Remove(modulo);
            await context.SaveChangesAsync();
        }
    }
}

using ClinicaBlazor.Data;
using ClinicaBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaBlazor.Services
{
    public class UsuarioService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public UsuarioService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Usuarios.ToListAsync();
        }

        public async Task GuardarAsync(Usuario usuario)
        {
            using var context = _dbFactory.CreateDbContext();

            if (usuario.Id == 0)
                context.Usuarios.Add(usuario);
            else
                context.Usuarios.Update(usuario);

            await context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();

            var u = await context.Usuarios.FindAsync(id);
            if (u != null)
            {
                context.Usuarios.Remove(u);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Usuarios.FindAsync(id);
        }
    }
    
}
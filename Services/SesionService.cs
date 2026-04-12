using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace ClinicaBlazor.Services
{
    public class SesionService
    {
        private readonly ProtectedSessionStorage _sessionStorage;

        public SesionService(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> HaySesionAsync()
        {
            var resultado = await _sessionStorage.GetAsync<string>("usuario_nombre");
            return resultado.Success && !string.IsNullOrWhiteSpace(resultado.Value);
        }

        public async Task<string?> ObtenerNombreAsync()
        {
            var resultado = await _sessionStorage.GetAsync<string>("usuario_nombre");
            return resultado.Success ? resultado.Value : null;
        }

        public async Task<string?> ObtenerPerfilAsync()
        {
            var resultado = await _sessionStorage.GetAsync<string>("usuario_perfil");
            return resultado.Success ? resultado.Value : null;
        }

        public async Task CerrarSesionAsync()
        {
            await _sessionStorage.DeleteAsync("usuario_id");
            await _sessionStorage.DeleteAsync("usuario_nombre");
            await _sessionStorage.DeleteAsync("usuario_username");
            await _sessionStorage.DeleteAsync("usuario_perfil");
        }
    }
}
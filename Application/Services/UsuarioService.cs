using Domain;
using Observability.Splunk;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuariosApi _usuariosApi;

        public UsuarioService(IUsuariosApi usuariosApi)
        {
            _usuariosApi = usuariosApi;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            Logger.LogInfo("Chamando _usuariosApi");
            return await _usuariosApi.GetUsuarios();
        }
    }
}

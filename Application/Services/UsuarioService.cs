using Domain;

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
            return await _usuariosApi.GetUsuarios();
        }
    }
}

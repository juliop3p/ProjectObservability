using Refit;

namespace Domain
{
    public interface IUsuariosApi
    {
        [Get("/api/usuarios")]
        public Task<List<Usuario>> GetUsuarios();
    }
}

using Domain;

namespace Domain
{
    public interface IUsuarioService
    {
        public Task<List<Usuario>> GetUsuarios();
    }
}
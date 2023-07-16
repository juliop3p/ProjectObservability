using Domain;
using Microsoft.Extensions.Options;
using Observability.Splunk;
using Refit;

namespace Infrastructure.ExternalApis
{
    public class UsuariosApi : IUsuariosApi
    {
        private readonly IUsuariosApi _usuariosApi;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UsuariosApi(IOptions<ApiOptions> apiOptions)
        {
            HttpClient httpClient = new HttpClient(new ApiExternalRequestResponseMiddleware());
            _baseUrl = apiOptions.Value.BaseUrl;
            httpClient.BaseAddress = new Uri(_baseUrl);

            _usuariosApi = RestService.For<IUsuariosApi>(httpClient); ;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            Logger.LogInfo("Chamando API de usuarios");

            var usuarios = await _usuariosApi.GetUsuarios();

            Logger.LogInfo(usuarios);

            return usuarios;
        }
    }
}

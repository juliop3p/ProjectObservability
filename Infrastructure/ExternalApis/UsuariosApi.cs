using Domain;
using Microsoft.Extensions.Options;
using Observability;
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
            return await _usuariosApi.GetUsuarios();
        }
    }
}

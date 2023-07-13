using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Observability.Splunk;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            Logger.LogInfo("Criando UsuariosController");
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        
        {
            Logger.LogInfo("Iniciando UsuariosController");
            List<Usuario> usuarios = await _usuarioService.GetUsuarios();

            return Ok(usuarios);
        }
    }
}

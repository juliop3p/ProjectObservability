using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        
        {
            List<Usuario> usuarios = await _usuarioService.GetUsuarios();

            return Ok(usuarios);
        }
    }
}
